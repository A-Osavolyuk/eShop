using eShop.AuthWebApi.Data;
using eShop.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShop.AuthWebApi.Services
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
            throw new NotImplementedException();
        }

        private List<Claim> SetClaims(AppUser user)
        {
            if (user is not null)
            {
                var list = new List<Claim>() 
                {
                    new Claim(JwtRegisteredClaimNames.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                };

                return list;
            }

            return new List<Claim>();
        }
    }
}
