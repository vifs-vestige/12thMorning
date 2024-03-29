﻿using System;
using System.Threading.Tasks;
using _12thMorning.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(_12thMorning.Areas.Identity.IdentityHostingStartup))]
namespace _12thMorning.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {


            var temp = ServerVersion.AutoDetect("Server=localhost;Database=12thmorning;Uid=12thmorning;");
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<_12thMorningContext>(options =>
                    options.UseMySql(@"Server = localhost; Database = 12thmorning; Uid = 12thmorning;", temp)
                    );


                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<_12thMorningContext>();
                services.AddAuthentication();
                services.AddAuthorization();

                //probly need to figure out better way to populate this data, but whatever for now
                //CreateRoles(services.BuildServiceProvider());
            });



        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleNames = new string[] { "admin" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var powerUser = await UserManager.FindByNameAsync("vifs_vestige");
            if (powerUser != null)
            {
                await UserManager.AddToRoleAsync(powerUser, "admin");
            }
        }
    }
}