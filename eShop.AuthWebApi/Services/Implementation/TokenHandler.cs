using eShop.AuthWebApi.Services.Interfaces;
using eShop.Domain.Common;
using eShop.Domain.Entities;
using eShop.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShop.AuthWebApi.Services.Implementation
{
    public class TokenHandler : ITokenHandler
    {
        private readonly JwtOptions jwtOptions;

        public TokenHandler(IOptions<JwtOptions> options)
        {
            jwtOptions = options.Value;
        }

        public string GenerateToken(AppUser user)
        {
            if (user is not null)
            {
                var handler = new JwtSecurityTokenHandler();

                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    algorithm: SecurityAlgorithms.HmacSha256Signature);

                var token = handler.WriteToken(new JwtSecurityToken(
                    audience: jwtOptions.Audience,
                    issuer: jwtOptions.Issuer,
                    claims: SetClaims(user),
                    expires: DateTime.Now.AddSeconds(jwtOptions.ExpirationSeconds),
                    signingCredentials: signingCredentials));

                return token;
            }

            return "";
        }

        public string RefreshToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();

                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    algorithm: SecurityAlgorithms.HmacSha256Signature);

                var newToken = handler.WriteToken(new JwtSecurityToken(
                    audience: jwtOptions.Audience,
                    issuer: jwtOptions.Issuer,
                    claims: DecryptToken(token),
                    expires: DateTime.Now.AddSeconds(jwtOptions.ExpirationSeconds),
                    signingCredentials: signingCredentials));

                return newToken;
            }

            return "";
        }

        private List<Claim> SetClaims(AppUser user)
        {
            if (user is not null)
            {
                var claims = new List<Claim>()
                {
                    new (CustomClaimTypes.UserName, user.UserName ?? "None"),
                    new (JwtRegisteredClaimNames.Email, user.Email ?? "None"),
                    new (CustomClaimTypes.Id, user.Id),
                }; 
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
                };

                return claims;
            }

            return new List<Claim>();
        }
    }
}
