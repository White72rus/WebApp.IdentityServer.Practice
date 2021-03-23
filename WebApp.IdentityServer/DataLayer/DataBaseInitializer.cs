using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace WebApp.IdentityServer.DataLayer
{
    public static class DataBaseInitializer
    {
        public static async void Init(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //var userIs = await userManager.FindByNameAsync("Admin");

            var user = new IdentityUser
            {
                UserName = "User",
            };
            var admin = new IdentityUser
            {
                UserName = "Admin",
            };

            var result = userManager.CreateAsync(user, "qwe123").GetAwaiter().GetResult();
            var result1 = userManager.CreateAsync(admin, "admin123").GetAwaiter().GetResult();

            if (result1.Succeeded)
            {
                userManager.AddClaimAsync(admin, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
            }

            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User")).GetAwaiter().GetResult();
            }
        }
    }
}
