using eShop.AuthWebApi.Data;
using eShop.Domain.Entities;

namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface ITokenHandler
    {
        public string GenerateToken(AppUser user);
        public string RefreshToken(string token);
    }
}
