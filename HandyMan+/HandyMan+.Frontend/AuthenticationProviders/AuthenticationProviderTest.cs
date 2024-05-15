using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Orders.Frontend.AuthenticationProviders
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            await Task.Delay(3000);
            var anonimous = new ClaimsIdentity();
            var user = new ClaimsIdentity(authenticationType: "Admin");
            var admin = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Juan"),
                new Claim("LastName", "Zulu"),
                new Claim(ClaimTypes.Name, "isra@yopmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(admin)));
        }
    }
}
