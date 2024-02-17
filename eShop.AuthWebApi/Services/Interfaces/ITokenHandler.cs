using eShop.AuthWebApi.Data;
using eShop.Domain.Common;

namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface ITokenHandler
    {
        public string GenerateToken(AppUser user);
        public string RefreshToken(string token);
    }
}
