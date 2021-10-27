using System;
using _12thMorning.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(_12thMorning.Areas.Identity.IdentityHostingStartup))]
namespace _12thMorning.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            

            var temp = ServerVersion.AutoDetect("Server=localhost;Database=12thmorning;Uid=12thmorning;");
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<_12thMorningContext>(options =>
                options.UseMySql(@"Server = localhost; Database = 12thmorning; Uid = 12thmorning;", temp));


                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<_12thMorningContext>();
            });


        }
    }
}