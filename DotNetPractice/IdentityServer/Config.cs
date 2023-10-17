using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        private const string API_NAME = "NorthwindAPI";
        private const string CLIENT_NAME = "test_client";

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("read"),
            new ApiScope("write")
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource(API_NAME)
            {
                Scopes = new List<string>()
                {
                    "read",
                    "write"
                }
            }
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client()
            {
                ClientId = CLIENT_NAME,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { API_NAME, "read", "write" },
                AccessTokenLifetime = 86400,
                AllowedCorsOrigins = new List<string>()
                {
                    "https://localhost:5001"
                }
            }
        };
    }
}
