using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(CS, option => {
                    option.Authority = "https://localhost:5001";
                    option.ClientId = "web_site";
                    option.ClientSecret = "web_site_secret";
                    option.SaveTokens = true;
                    option.ResponseType = TYPE;
                });

            services.AddControllersWithViews();
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
