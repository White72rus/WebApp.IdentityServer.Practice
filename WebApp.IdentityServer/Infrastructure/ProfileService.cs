using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.IdentityServer.Infrastructure
{
    public class ProfileService : IProfileService
    {
        private readonly UserService _userService;
        private readonly IServiceProvider _serviceProvider;

        public ProfileService(UserService userService, IServiceProvider serviceProvider)
        {
            _userService = userService;
            _serviceProvider = serviceProvider;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userService.Get();

            var userManager = _serviceProvider.GetService<UserManager<IdentityUser>>();

            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                var userIs = userManager.FindByNameAsync(user.Name).GetAwaiter().GetResult();
                var userClaims = userManager.GetClaimsAsync(userIs).GetAwaiter().GetResult();

                var claims = new List<Claim>(){
                new Claim(ClaimTypes.Role, "Administrator"),
                };

                context.IssuedClaims.AddRange(userClaims);
            }
            
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
