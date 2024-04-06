using eShop.Domain.Common;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eShop.Infrastructure.Account
{
    public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState anonymous = new(new ClaimsPrincipal());
        private readonly ITokenProvider tokenProvider;
        private readonly IAuthenticationService authenticationService;

        public ApplicationAuthenticationStateProvider(ITokenProvider tokenProvider, IAuthenticationService authenticationService)
        {
            this.tokenProvider = tokenProvider;
            this.authenticationService = authenticationService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(JwtHandler.Token))
                {
                    var token = DecryptToken(JwtHandler.Token);

                    if (token is not null || token!.Claims.Any())
                    {
                        var valid = IsValid(token!);

                        if (valid)
                        {
                            var claims = SetClaims(token);

                            if (claims.Any())
                            {
                                return await Task.FromResult(
                                    new AuthenticationState(new ClaimsPrincipal(
                                        new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme))));
                            }

                            return await Task.FromResult(anonymous);
                        }

                        //return await RefreshToken(JwtHandler.Token);
                    }

                    return await Task.FromResult(anonymous);
                }

                return await Task.FromResult(anonymous);
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
                claimsPrincipal = new(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
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

        private bool IsValid(JwtSecurityToken token)
        {
            var expValue = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value;
            var expMilliseconds = Convert.ToInt64(expValue);
            var expData = DateTimeOffset.FromUnixTimeSeconds(expMilliseconds);

            return DateTimeOffset.Now < expData;
        }

        private async Task<AuthenticationState> RefreshToken(string expiredToken)
        {
            var result = await authenticationService.RefreshToken(new RefreshTokenRequest() { Token = expiredToken });

            if (result.IsSucceeded)
            {
                var response = JsonConvert.DeserializeObject<RefreshTokenResponse>(result.Result!.ToString()!)!;
                var newToken = response.Token;

                if (!string.IsNullOrEmpty(newToken))
                {
                    var token = DecryptToken(newToken);

                    if (token is not null || token!.Claims.Any())
                    {
                        var claims = SetClaims(token);

                        if (claims.Any())
                        {
                            return await Task.FromResult(
                                new AuthenticationState(new ClaimsPrincipal(
                                    new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme))));
                        }
                    }
                }
            }

            return await Task.FromResult(anonymous);
        }

        public async Task LogOutAsync()
        {
            await tokenProvider.RemoveTokenAsync();
            await UpdateAuthenticationState("");
        }
    }
}
