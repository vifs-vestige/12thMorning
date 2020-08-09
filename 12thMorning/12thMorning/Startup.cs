using _12thMorning.Data;
using _12thMorning.Services;
using Blazored.LocalStorage;
using BlazorStrap;
using BlazorStyled;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net.Http;

namespace _12thMorning {
    public class Startup {
        private static bool isDev = false;
        public static bool IsDev { get { return isDev; } }
        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<BlogService>();
            services.AddSingleton<CommentService>();
            services.AddSingleton<QueslarService>();
            services.AddScoped<SessionStorage>();
            services.AddBootstrapCSS();
            services.AddBlazorStyled();
            services.AddBlazoredLocalStorage();

            services.AddServerSideBlazor().AddCircuitOptions(o =>
            {
                if (Env.IsDevelopment()) //only add details when debugging
                {
                    o.DetailedErrors = true;
                }
            });

            services.Configure<ForwardedHeadersOptions>(options => {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            if (!services.Any(x => x.ServiceType == typeof(HttpClient))) {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped<HttpClient>(s => {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    NavigationManager navman = s.GetRequiredService<NavigationManager>();
                    return new HttpClient {
                        BaseAddress = new Uri(navman.BaseUri)
                    };
                });
            }

            //services.AddDbContextPool<_12thMorningContext>( 
            //    options => options.UseMySql("Server=localhost;Database=12thmorning;Uid=12thmorning;", 
            //    mySqlOptions => {
            //        mySqlOptions.ServerVersion(new Version(10, 1, 41), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MariaDb); // replace with your Server Version and Type
            //    }
            //));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                Startup.isDev = true;
            }
            else {
                app.UseExceptionHandler("/Tools/Error");
                //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //    app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseFileServer();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/Tools/_Host");
            });
        }
    }
}
