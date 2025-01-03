﻿using eShop.Domain.Common.Security;
using eShop.Domain.Models.Store;
using eShop.Domain.Requests.AuthApi.Auth;
using eShop.Domain.Responses.AuthApi.Auth;

namespace eShop.Infrastructure.Account;

public class ApplicationAuthenticationStateProvider(
    ITokenProvider tokenProvider,
    IAuthenticationService authenticationService,
    ILocalDataAccessor localDataAccessor) : AuthenticationStateProvider
{
    private readonly AuthenticationState anonymous = new(new ClaimsPrincipal());
    private readonly ITokenProvider tokenProvider = tokenProvider;
    private readonly IAuthenticationService authenticationService = authenticationService;
    private readonly ILocalDataAccessor localDataAccessor = localDataAccessor;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(AuthenticationHandler.Token))
            {
                return await Task.FromResult(anonymous);
            }

            var token = DecryptToken(AuthenticationHandler.Token);

            if (token is null || !token.Claims.Any())
            {
                return await Task.FromResult(anonymous);
            }

            var valid = IsValid(token);

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

    public async Task UpdateAuthenticationStateAsync(string token)
    {
        var claimsPrincipal = new ClaimsPrincipal();

        if (string.IsNullOrEmpty(token))
        {
            AuthenticationHandler.Token = string.Empty;
        }
        else
        {
            AuthenticationHandler.Token = token;
            await tokenProvider.SetTokenAsync(token);

            var rawToken = DecryptToken(token)!;
            var claims = SetClaims(rawToken);
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
            var claims = SetClaims(rawToken);
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
        var claims = token.Claims.ToList();

        var output = new List<Claim>()
        {
            new(CustomClaimTypes.Id, claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Id)!.Value),
            new(ClaimTypes.Name, claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserName)!.Value),
            new(ClaimTypes.Email, claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)!.Value),
            new(ClaimTypes.MobilePhone, claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)!.Value),
        };

        var roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
        var permissions = claims.Where(x => x.Type == CustomClaimTypes.Permission).Select(x => x.Value).ToList();

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

    private async Task WriteToLocalStorageAsync(List<Claim> claims)
    {
        var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
        var username = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;
        var phoneNumber = claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)!.Value;
        var id = claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Id)!.Value;
        var roles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();
        var permissions = claims.Where(x => x.Type == CustomClaimTypes.Permission).Select(x => x.Value).ToList();

        await localDataAccessor.WriteUserDataAsync(new UserStore()
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

        if (result.Success)
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
        await tokenProvider.ClearAsync();
        await localDataAccessor.ClearAsync();
        await UpdateAuthenticationStateAsync(string.Empty);
    }
}