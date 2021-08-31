using Domain.ApiSecurity;
using Domain.Api;
using Domain.Database;
using Domain.Resource.Business;
using Domain.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Net.Http;

namespace Domain.Resource.Web
{
    public static class ServiceRegistryExtension
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
            services.ConfigureSwagger();

            return services;
        }
        public static IServiceCollection AddInstance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUnitOfWork, RepoSqlSrvDbUnitOfWork>();
            services.AddSingleton<IConnection, RepoSqlSrvDbConnection>();

            services.AddSingleton<INews, NewsManager>();
            services.AddSingleton<IGlobalParameter, GlobalParameterManager>();

            // Supply parameters for swagger and security
            services.AddSingleton<ISecurityConfiguration, SecurityConfigurationClient>(
                p => new SecurityConfigurationClient(config =>
                {
                    var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
                    var apiVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    var globalParameter = p.GetRequiredService<IGlobalParameter>().GetAll().Result;
                    var httpClient = p.GetRequiredService<IHttpClientFactory>().CreateClient();


                    config.IdentityAddress = globalParameter.Where(g => g.ParameterId == "IdentityAddress").First().Value;
                    config.ResourceName = globalParameter.Where(g => g.ParameterId == "ResourceName").First().Value;
                    config.ResourceDisplayName = globalParameter.Where(g => g.ParameterId == "ResourceDisplayName").First().Value;
                    config.ResourceVersion = apiVersion;
                    config.SwaggerDefaultClientId = appSettings.SwaggerDefaultClientId;
                    config.SwaggerDefaultClientSecret = appSettings.SwaggerDefaultClientSecret;
                }));
            // Supply parameters for service discovery
            services.AddSingleton<IServiceDiscoveryConfiguration, ServiceDiscoveryConfigurationClient>(
                p => new ServiceDiscoveryConfigurationClient(config =>
                {
                    var globalParameter = p.GetRequiredService<IGlobalParameter>().GetAll().Result;

                    config.Address = globalParameter.Where(g => g.ParameterId == "ConsulAddress").First().Value;
                }));
            services.AddSingleton<IServiceDiscoveryRegistration, ServiceDiscoveryRegistrationClient>(
                p => new ServiceDiscoveryRegistrationClient(config =>
                {
                    var globalParameter = p.GetRequiredService<IGlobalParameter>().GetAll().Result;
                    var resourceUrl = globalParameter.Where(g => g.ParameterId == "ResourceUrl").First().Value;
                    Uri apiUrl = new(resourceUrl);

                    config.ID = globalParameter.Where(g => g.ParameterId == "ResourceName").First().Value;
                    config.Name = globalParameter.Where(g => g.ParameterId == "ResourceDisplayName").First().Value;
                    config.Port = apiUrl.Port;
                    config.Address = apiUrl.Host;
                }));

            return services;
        }
        public static IServiceCollection AddFilter(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            });

            return services;
        }
    }
}
