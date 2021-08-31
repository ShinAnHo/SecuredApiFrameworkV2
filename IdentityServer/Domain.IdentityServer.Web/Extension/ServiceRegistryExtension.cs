using Domain.ApiSecurity;
using Domain.Api;
using Domain.Database;
using Domain.IdentityServer.Business;
using Domain.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Domain.IdentityServer.Web
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
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IOauthManager, OauthManager>();
            services.AddSingleton<IClient, ClientManager>();
            services.AddSingleton<IClientSecret, ClientSecretManager>();
            services.AddSingleton<IApiResource, ApiResourceManager>();
            services.AddSingleton<IApiScope, ApiScopeManager>();
            services.AddSingleton<IGlobalParameter, GlobalParameterManager>();

            // Supply parameters for swagger and security
            services.AddSingleton<ISecurityConfiguration, SecurityConfigurationClient>(
                p => new SecurityConfigurationClient(config =>
                {
                    var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
                    var apiVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    var apiResource = p.GetRequiredService<IApiResource>().GetById(appSettings.ResourceId).Result;
                    var globalParameter = p.GetRequiredService<IGlobalParameter>().GetAll().Result;
                    
                    config.IdentityAddress = globalParameter.Where(g => g.ParameterID == "IdentityAddress").First().Value;
                    config.ResourceName = apiResource.Name;
                    config.ResourceDisplayName = apiResource.DisplayName;
                    config.ResourceVersion = apiVersion;
                    config.SwaggerDefaultClientId = appSettings.SwaggerDefaultClientId;
                    config.SwaggerDefaultClientSecret = appSettings.SwaggerDefaultClientSecret;
                }));
            // Supply parameters for service discovery
            services.AddSingleton<IServiceDiscoveryConfiguration, ServiceDiscoveryConfigurationClient>(
                p => new ServiceDiscoveryConfigurationClient(config =>
                {
                    var globalParameter = p.GetRequiredService<IGlobalParameter>().GetAll().Result;

                    config.Address = globalParameter.Where(g => g.ParameterID == "ConsulAddress").First().Value;
                }));
            services.AddSingleton<IServiceDiscoveryRegistration, ServiceDiscoveryRegistrationClient>(
                p => new ServiceDiscoveryRegistrationClient(config =>
                {
                    var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
                    var apiResource = p.GetRequiredService<IApiResource>().GetById(appSettings.ResourceId).Result;
                    Uri apiUrl = new(apiResource.Url);
                    
                    config.ID = apiResource.Name;
                    config.Name = apiResource.DisplayName;
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
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            return services;
        }
    }
}
