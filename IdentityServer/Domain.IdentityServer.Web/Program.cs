using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                    webBuilder.UseStartup<Startup>(); 

                    if(env != null)
                    {
                        webBuilder.ConfigureAppConfiguration(config =>
                            config.AddJsonFile($"appsettings.{env}.json"));
                    }

                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.AddServerHeader = false;
                    });
                });
    }
}
