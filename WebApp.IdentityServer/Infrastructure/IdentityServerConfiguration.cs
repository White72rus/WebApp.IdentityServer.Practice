﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace WebApp.IdentityServer.Infrastructure
{
    internal static class IdentityServerConfiguration
    {
        private const string URL = "/signin-oidc";
        internal static IEnumerable<Client> GetClients() => 
            // Создаем клиентов(учетки приложений) для IS4. 
            new List<Client> {
                new Client()
                {
                    Enabled = true,
                    ClientId = "smsis_portal_web",
                    ClientSecrets = {new Secret("smsis_portal_web".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {
                        "smsis.portal",
                        IdentityServerConstants.StandardScopes.OpenId,
                        //IdentityServerConstants.StandardScopes.Profile,
                    }
                },
                new Client()
                {
                    Enabled = true,
                    ClientId = "web_site",
                    ClientSecrets = {new Secret("web_site_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = {
                        "web.site",
                        "smsis.portal",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },

                    RedirectUris = { "https://localhost:10001/signin-oidc" },
                    RequireConsent = false,
                }
            };

        internal static IEnumerable<ApiResource> GetApiResources() {
            yield return new ApiResource("smsis.portal", "SMSIS Portal API") { Scopes = { "smsis.portal" } };
            yield return new ApiResource("web.site", "Web site") { Scopes = { "web.site" } };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
        }

        internal static IEnumerable<ApiScope> GetApiScopes()
        {
            yield return new ApiScope("smsis.portal", "SMSIS Portal API");
            yield return new ApiScope("web.site", "Web site");
        }
    }
}
