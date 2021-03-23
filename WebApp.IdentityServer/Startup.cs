using System;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WebApp.IdentityServer.DataLayer;
using WebApp.IdentityServer.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace WebApp.IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfiguration = configuration;
        }

        public IConfiguration AppConfiguration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = AppConfiguration.GetConnectionString("Development");
            services.AddDbContext<AppDbContext>(config => {
                config.UseMySql(connectionString, option => {
                    option.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    option.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
            })
            .AddIdentity<IdentityUser, IdentityRole>(config => {
                config.Password.RequireDigit = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredUniqueChars = 1;
                config.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<AppDbContext>();


            services.AddIdentityServer(option => {
                option.UserInteraction.LoginUrl = "/auth/login";    //https://localhost:5001
            })
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetApiScopes())
                .AddJwtBearerClientAuthentication()
                .AddDeveloperSigningCredential();

            //services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApp.IdentityServer", Version = "v1" });
            });

            services.AddCors();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp.IdentityServer v1"));
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            app.UseRouting();

            app.Use(async (context, next) => {

                await next.Invoke();
            });

            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
