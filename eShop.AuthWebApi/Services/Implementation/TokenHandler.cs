using eShop.Domain.Entities.Auth;
using eShop.Domain.Responses.Auth;

namespace eShop.AuthWebApi.Services.Implementation
{
    public class TokenHandler : ITokenHandler
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
            if (user is not null)
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

                await context.UserAuthenticationTokens.AddAsync(new() { UserId = user.Id, Token = refreshToken });
                await context.SaveChangesAsync();

                return new TokenResponse()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }

            return new TokenResponse();
        }

        private List<Claim> SetClaims(AppUser user, List<string> roles, List<string> permissions)
        {
            if (user is not null)
            {
                var claims = new List<Claim>()
                {
                    new (CustomClaimTypes.UserName, user.UserName ?? "None"),
                    new (JwtRegisteredClaimNames.Email, user.Email ?? "None"),
                    new (CustomClaimTypes.Id, user.Id),
                    new (ClaimTypes.MobilePhone, user.PhoneNumber ?? "")
                };

                if (roles.Any())
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                if (permissions.Any())
                {
                    foreach (var permission in permissions)
                    {
                        claims.Add(new Claim(CustomClaimTypes.Permission, permission));
                    }
                }

                return claims;
            }

            return new List<Claim>();
        }

        private List<Claim> DecryptToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (!string.IsNullOrEmpty(token) && handler.CanReadToken(token))
            {
                var rawToken = handler.ReadJwtToken(token);

                var claims = new List<Claim>()
                {
                    new (CustomClaimTypes.UserName, rawToken.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserName)!.Value),
                    new (JwtRegisteredClaimNames.Email, rawToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)!.Value),
                    new (CustomClaimTypes.Id, rawToken.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Id)!.Value),
                    new (ClaimTypes.MobilePhone, rawToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)!.Value),
                };

                var roles = rawToken.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
                var permissions = rawToken.Claims.Where(x => x.Type == CustomClaimTypes.Permission).Select(x => x.Value);

                if (roles.Any())
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }

                if (permissions.Any())
                {
                    foreach (var permission in permissions)
                    {
                        claims.Add(new Claim(CustomClaimTypes.Permission, permission));
                    }
                }

                return claims;
            }

            return new List<Claim>();
        }
    }
}
