using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eShop.BlazorWebUI.Auth
{
    public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState anonymous = new(new ClaimsPrincipal());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(JwtHandler.Token))
                    return await Task.FromResult(anonymous);

                var rawToken = DecryptToken(JwtHandler.Token);

                if (rawToken is null || !rawToken.Claims.Any())
                    return await Task.FromResult(anonymous);

                var claims = SetClaims(rawToken);

                if (!claims.Any())
                    return await Task.FromResult(anonymous);

                return await Task.FromResult(
                    new AuthenticationState(new ClaimsPrincipal(
                        new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme))));
            }
            catch (Exception)
            {
                return await Task.FromResult(anonymous);
            }
        }

        private JwtSecurityToken? DecryptToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!string.IsNullOrEmpty(token) && handler.CanReadToken(token))
            {
                var rawToken = handler.ReadJwtToken(token);
                return rawToken;
            }

            return new JwtSecurityToken();
        }

        private List<Claim> SetClaims(JwtSecurityToken token)
        {
            if (token is not null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name).Value),
                    new Claim(ClaimTypes.Email, token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value),
                    new Claim("Id", token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value),
                };

                return claims;
            }

            return new List<Claim>();
        }
    }
}
