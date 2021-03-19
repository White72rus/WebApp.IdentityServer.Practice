using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace WebApp.IdentityServer.DataLayer
{
    public static class DataBaseInitializer
    {
        public static void Init(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = new IdentityUser
            {
                UserName = "Admin"
            };

            var result = userManager.CreateAsync(user, "qwe123").GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator" )).GetAwaiter().GetResult(); 
            }
        }
    }
}
