using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace _12thMorning
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            CreateWebHostBuilder(args).Build().Run();
            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseIISIntegration()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    //.usewebroot("wwwroot")
            //    .UseStartup<Startup>()
            //    .Build();

            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {

                    webBuilder.UseStartup<Startup>();
                    if (args.Contains("dev")) {
                        webBuilder.UseSetting(WebHostDefaults.DetailedErrorsKey, "true");
                    }
                    //webBuilder.UseKestrel();
                });


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStaticWebAssets()
                .UseStartup<Startup>();
    }
}
