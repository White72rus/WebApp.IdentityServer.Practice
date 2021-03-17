using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace WebApp.IdentityServer.Infrastructure
{
    public static class Configuration
    {
        internal static IEnumerable<Client> GetClients() => 
            // Создаем клиентов(учетки приложений) для IS4. 
            new List<Client> {
                new Client()
                {
                    ClientId = "smsis_portal_web",
                    ClientSecrets = {new Secret("smsis_portal_web".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        "SMSISPortal",
                    },
                }
            };

        internal static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource> {
                new ApiResource("SMSISPortal"),
            };

        internal static IEnumerable<IdentityResource> GetIdentityResources() => 
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
            };
    }
}
