using System;
using System.Linq;
using System.Net.Http;
using _12thMorning.Services;
using Blazored.LocalStorage;
using BlazorStrap;
using BlazorStyled;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using _12thMorning.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace _12thMorning
{
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
            services.AddScoped<SessionStorage>();
            services.AddBootstrapCss();
            services.AddBlazorStyled();
            services.AddBlazoredLocalStorage();
            services.AddHttpClient();
            services.AddScoped<TokenProvider>();

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>();
                //.AddEntityFrameworkStores<_12thMorningContext>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Identity.Application";
                
            //})
            //    //.AddJwtBearer(options =>
            //    //{
            //    //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    //    {
            //    //        ValidateIssuer = true,
            //    //        ValidateAudience = true,
            //    //        ValidateLifetime = true,
            //    //        ValidateIssuerSigningKey = true,
            //    //        ValidIssuer = Configuration["JwtIssuer"],
            //    //        ValidAudience = Configuration["JwtAudience"],
            //    //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
            //    //    };
            //    //})
            //    .AddCookie();

            //var temp = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("admin").Build();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("IsAdmin", temp);
            //});

            
            services.AddServerSideBlazor();

            //services.Configure<OpenIdConnectOptions>(options =>
            //{
            //    options.ResponseType = OpenIdConnectResponseType.Code;
            //    options.SaveTokens = true;
            //    options.ClientId = "";
            //});

            services.Configure<ForwardedHeadersOptions>(options => {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            if (!services.Any(x => x.ServiceType == typeof(HttpClient))) {
                services.AddScoped<HttpClient>(s => {
                    NavigationManager navman = s.GetRequiredService<NavigationManager>();
                    return new HttpClient {
                        BaseAddress = new Uri(navman.BaseUri)
                    };
                });
            }


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

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseFileServer();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/Tools/_Host");
            });
        }
    }
}
