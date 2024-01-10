using IdentityModel;
using IdentityServer4.Models;

namespace Server.Configurations
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
           };

        public static IEnumerable<ApiResource> ApiResources
            => new List<ApiResource>
            {
                new ApiResource("list", "Server"),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> {
                new ApiScope("list", "Server"),
                new ApiScope(name: "read", displayName: "Read your data."),
                new ApiScope(name: "write", displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id",
                    ClientName = "client_name",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "read", "write", "list" }
                },

                new Client
                {
                    ClientId = "list",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:7270/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:7270/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:7270/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "list" },
                },
            };
    }
}
