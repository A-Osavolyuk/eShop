using eShop.Auth.Api.Entities;
using ClaimTypes = eShop.Domain.Common.Security.ClaimTypes;

namespace eShop.Auth.Api.Services;

internal sealed class TokenHandler : ITokenHandler
{
    private readonly JwtOptions jwtOptions;
    private readonly AuthDbContext context;

    public TokenHandler(IOptions<JwtOptions> options, AuthDbContext context)
    {
        jwtOptions = options.Value;
        this.context = context;
    }

    public async Task<TokenResponse> GenerateTokenAsync(AppUser user, List<string> roles, List<string> permissions)
    {
        var handler = new JwtSecurityTokenHandler();

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(jwtOptions.Key)),
            algorithm: SecurityAlgorithms.HmacSha256Signature);

        var accessToken = handler.WriteToken(new JwtSecurityToken(
            audience: jwtOptions.Audience,
            issuer: jwtOptions.Issuer,
            claims: SetClaims(user, roles, permissions),
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signingCredentials));

        var refreshToken = handler.WriteToken(new JwtSecurityToken(
            audience: jwtOptions.Audience,
            issuer: jwtOptions.Issuer,
            claims: SetClaims(user, roles, permissions),
            expires: DateTime.Now.AddSeconds(jwtOptions.ExpirationSeconds),
            signingCredentials: signingCredentials));

        await context.SecurityTokens.AddAsync(new() { UserId = user.Id, Token = refreshToken });
        await context.SaveChangesAsync();

        return new TokenResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private List<Claim> SetClaims(AppUser user, List<string> roles, List<string> permissions)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.UserName, user.UserName ?? "None"),
            new(JwtRegisteredClaimNames.Email, user.Email ?? "None"),
            new(ClaimTypes.Id, user.Id),
            new(System.Security.Claims.ClaimTypes.MobilePhone, user.PhoneNumber ?? "")
        };

        if (roles.Any())
        {
            foreach (var role in roles)
            {
                claims.Add(new Claim(System.Security.Claims.ClaimTypes.Role, role));
            }
        }

        if (permissions.Any())
        {
            foreach (var permission in permissions)
            {
                claims.Add(new Claim(ClaimTypes.Permission, permission));
            }
        }

        return claims;
    }

    private JwtSecurityToken? DecryptToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        if (!string.IsNullOrEmpty(token) && handler.CanReadToken(token))
        {
            var rawToken = handler.ReadJwtToken(token);

            return rawToken;
        }
        else
        {
            return null;
        }
    }

    private DateTime? GetTokenExpirationDate(JwtSecurityToken token)
    {
        return token.ValidTo;
    }

    private List<Claim> GetClaimsFromToken(JwtSecurityToken? token)
    {
        if (token is null)
        {
            return new List<Claim>();
        }

        var claims = new List<Claim>()
        {
            new(ClaimTypes.UserName,
                token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserName)!.Value),
            new(JwtRegisteredClaimNames.Email,
                token.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)!.Value),
            new(ClaimTypes.Id, token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Id)!.Value),
            new(System.Security.Claims.ClaimTypes.MobilePhone,
                token.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.MobilePhone)!.Value),
        };

        var roles = token.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Role).Select(x => x.Value)
            .ToList();
        var permissions = token.Claims.Where(x => x.Type == ClaimTypes.Permission).Select(x => x.Value).ToList();

        if (roles.Any())
        {
            foreach (var role in roles)
            {
                claims.Add(new Claim(System.Security.Claims.ClaimTypes.Role, role));
            }
        }

        if (permissions.Any())
        {
            foreach (var permission in permissions)
            {
                claims.Add(new Claim(ClaimTypes.Permission, permission));
            }
        }

        return claims;
    }

    public TokenResponse? RefreshToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var rawToken = DecryptToken(token);

        if (rawToken is null)
        {
            return null;
        }

        var claims = GetClaimsFromToken(rawToken);
        var validateTo = GetTokenExpirationDate(rawToken);

        var handler = new JwtSecurityTokenHandler();

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(jwtOptions.Key)),
            algorithm: SecurityAlgorithms.HmacSha256Signature);

        var accessToken = handler.WriteToken(new JwtSecurityToken(
            audience: jwtOptions.Audience,
            issuer: jwtOptions.Issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signingCredentials));

        var refreshToken = handler.WriteToken(new JwtSecurityToken(
            audience: jwtOptions.Audience,
            issuer: jwtOptions.Issuer,
            claims: claims,
            expires: validateTo,
            signingCredentials: signingCredentials));

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}