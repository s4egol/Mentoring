using IdentityServer4;
using IdentityServer4.Models;

namespace Identity
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("catalog-swagger-API", "Catalog swagger API"),
                new ApiScope("cart-swagger-API", "Cart swagger API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("catalog-swagger-API")
                {
                    Scopes = { "catalog-swagger-API" }
                },
                new ApiResource("cart-swagger-API")
                {
                    Scopes = { "cart-swagger-API" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new()
                {
                    ClientId = "catalog-swagger-api-client-id",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedCorsOrigins =
                    {
                        "https://localhost:3333"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "catalog-swagger-API"
                    }
                },
                new()
                {
                    ClientId = "cart-swagger-api-client-id",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequireClientSecret = false,
                    AllowedCorsOrigins =
                    {
                        "https://localhost:7777"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "cart-swagger-API"
                    }
                }
            };
    }
}
