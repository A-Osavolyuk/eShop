using eShop.Domain.Common;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.DTOs.Responses.Auth;
using eShop.Domain.Interfaces;
using eShop.Domain.Models;
using eShop.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eShop.Infrastructure.Account
{
    public class ApplicationAuthenticationStateProvider(
        ITokenProvider tokenProvider,
        IAuthenticationService authenticationService,
        ILocalDataAccessor localDataAccessor,
        ICookieManager cookieManager) : AuthenticationStateProvider
    {
        private readonly AuthenticationState anonymous = new(new ClaimsPrincipal());
        private readonly ITokenProvider tokenProvider = tokenProvider;
        private readonly IAuthenticationService authenticationService = authenticationService;
        private readonly ILocalDataAccessor localDataAccessor = localDataAccessor;
        private readonly ICookieManager cookieManager = cookieManager;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(AuthenticationHandler.Token))
                {
                    return await Task.FromResult(anonymous);
                }

                var token = DecryptToken(AuthenticationHandler.Token);

                if (token is null || !token!.Claims.Any())
                {
                    return await Task.FromResult(anonymous);
                }

                var valid = IsValid(token!);

                if (!valid)
                {
                    await LogOutAsync();
                    return await Task.FromResult(anonymous);
                }

                var claims = SetClaims(token);

                if (!claims.Any())
                {
                    return await Task.FromResult(anonymous);
                }

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

            if (string.IsNullOrEmpty(token))
            {
                AuthenticationHandler.Token = "";
            }
            else
            {
                AuthenticationHandler.Token = token;
                await tokenProvider.SetTokenAsync(token);

                var rawToken = DecryptToken(token)!;
                var claims = SetClaims(rawToken)!;
                await WriteToLocalStorageAsync(claims);
                claimsPrincipal = new(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task LoginAsync(string accessToken, string refreshToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                AuthenticationHandler.Token = "";
            }
            else
            {
                AuthenticationHandler.Token = refreshToken;
                await tokenProvider.SetTokenAsync(refreshToken);

                var rawToken = DecryptToken(accessToken)!;
                var claims = SetClaims(rawToken)!;
                await WriteToLocalStorageAsync(claims);
                claimsPrincipal = new(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            }

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
                    new(ClaimTypes.MobilePhone, claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)!.Value),
                };

                var roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
                var permissions = claims.Where(x => x.Type == CustomClaimTypes.Permission).Select(x => x.Value);

                if (roles.Any())
                {
                    foreach (var role in roles)
                    {
                        output.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                if (permissions.Any())
                {
                    foreach (var permission in permissions)
                    {
                        output.Add(new Claim(CustomClaimTypes.Permission, permission));
                    }
                }

                return output;
            }

            return new List<Claim>();
        }

        private async Task WriteToLocalStorageAsync(List<Claim> Claims)
        {

            var email = Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
            var username = Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;
            var phoneNumber = Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)!.Value;
            var id = Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Id)!.Value;
            var roles = Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
            var permissions = Claims.Where(x => x.Type == CustomClaimTypes.Permission).Select(x => x.Value).ToList();

            await localDataAccessor.WriteUserDataAsync(new UserData()
            {
                PhoneNumber = phoneNumber,
                Email = email,
                UserName = username,
                UserId = id,
                Roles = roles,
                Permissions = permissions
            });
        }

        private bool IsValid(JwtSecurityToken token)
        {
            var expValue = token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value;
            var expMilliseconds = Convert.ToInt64(expValue);
            var expData = DateTimeOffset.FromUnixTimeSeconds(expMilliseconds);

            return DateTimeOffset.Now < expData;
        }

        private async Task<AuthenticationState> RefreshTokenAsync(string expiredToken)
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
                            AuthenticationHandler.Token = newToken;
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
            await localDataAccessor.RemoveDataAsync();
            await UpdateAuthenticationState(string.Empty);
        }
    }
}
