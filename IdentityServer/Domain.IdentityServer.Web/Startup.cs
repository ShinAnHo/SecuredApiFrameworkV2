using Domain.Database;
using Domain.IdentityServer.Business;
using Domain.IdentityServer.Data;
using Domain.IdentityServer.Oauth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Api;
using Domain.ServiceDiscovery;
using Domain.ApiSecurity;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Domain.IdentityServer.Web
{
    public class Startup
    {
        readonly string defaultOrigins = "_defaultOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfiguration(Configuration);
            services.AddInstance(Configuration);
            services.AddServiceDiscovery();
            services.AddFilter();
            services.AddSecurity();

            services.AddCors(options =>
            {
                options.AddPolicy(name: defaultOrigins, builder =>
                {
                    builder.WithOrigins("https://localhost:44397");
                    //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
                });
            });
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDevelopment();
            }
            else
            {
                app.UseServiceDiscovery();
            }

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseCors(defaultOrigins);

            app.UseAuthorization();

            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMiddleware<JwtMiddleware>();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"Libs")),
                RequestPath = new PathString("/libs")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
