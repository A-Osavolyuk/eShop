using eShop.Domain.Common;
using eShop.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eShop.Infrastructure.Account
{
    public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState anonymous = new(new ClaimsPrincipal());
        private readonly ITokenProvider tokenProvider;

        public ApplicationAuthenticationStateProvider(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(JwtHandler.Token))
                    return await Task.FromResult(anonymous);

                var token = DecryptToken(JwtHandler.Token);

                if (token is null || !token.Claims.Any())
                    return await Task.FromResult(anonymous);

                var claims = SetClaims(token);

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

        public async Task UpdateAuthenticationState(string token)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(token))
            {
                JwtHandler.Token = token;
                await tokenProvider.SetTokenAsync(token);

                var rawToken = DecryptToken(token)!;
                var claims = SetClaims(rawToken)!;
                claimsPrincipal = new (new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            }
            else
                JwtHandler.Token = "";
                
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
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
                var claims = token.Claims.ToList();

                var output = new List<Claim>()
                {
                    new(CustomClaimTypes.Id, claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Id)!.Value),
                    new(ClaimTypes.Name, claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserName)!.Value),
                    new(ClaimTypes.Email, claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)!.Value),
                };

                return output;
            }

            return [];
        }
    }
}
