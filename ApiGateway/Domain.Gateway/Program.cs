using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Gateway
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
                            config.AddJsonFile($"ocelot.{env}.json", optional: true, reloadOnChange: true));
                    }
                    else
                    {
                        webBuilder.ConfigureAppConfiguration(config =>
                            config.AddJsonFile($"ocelot.json", optional: true, reloadOnChange: true));
                    }
                })
            .ConfigureLogging(logging => logging.AddConsole());
    }
}
