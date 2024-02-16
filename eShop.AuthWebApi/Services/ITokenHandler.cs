using eShop.AuthWebApi.Data;

namespace eShop.AuthWebApi.Services
{
    public interface ITokenHandler
    {
        public string GenerateToken(AppUser user);
        public string RefreshToken(string token);
    }
}
