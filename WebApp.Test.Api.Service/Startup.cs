using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;

namespace WebApp.Test.Api.Service
{
    public class Startup
    {
        private readonly IConfiguration _configyration;

        private const string CS = "oidc";
        private const string TYPE = "code";

        public Startup(IConfiguration configyration)
        {
            _configyration = configyration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(option => {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CS;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, optios => {
                    optios.Cookie = new CookieBuilder() {
                        HttpOnly = true,
                        MaxAge = TimeSpan.FromMinutes(5),
                    };
                    //optios.AccessDeniedPath = "/Home/AccessDeniedMy/";
                })
                .AddOpenIdConnect(CS, option => {
                    option.Authority = "http://localhost:5000";
                    option.ClientId = "web_site";
                    option.ClientSecret = "web_site_secret";
                    option.Scope.Add("web.site");
                    option.SaveTokens = true;
                    option.ResponseType = TYPE;
                    option.UseTokenLifetime = true;
                    option.RequireHttpsMetadata = false;
                    // Получать клаймы в access_token
                    option.GetClaimsFromUserInfoEndpoint = true;

                    // Затягиваем клаймы в User Claim
                    option.ClaimActions.MapJsonKey(ClaimTypes.Role, ClaimTypes.Role);
                });

            services.AddAuthorization(config => {
                config.AddPolicy("Administrator", config => {
                    config.RequireClaim(ClaimTypes.Role, "Administrator");
                });

                config.AddPolicy("User", config => {
                    config.RequireClaim(ClaimTypes.Role, "User");
                });
            });

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                // Use "Always" for only HTTPS
                // Use "None" for only HTTP
                // Use "SameAsRequest" for HTTPS and HTTP in priority "HTTP"
                Secure = CookieSecurePolicy.SameAsRequest,
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
