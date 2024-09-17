namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface ITokenHandler
    {
        public string GenerateToken(AppUser user, List<string> roles);
        public string RefreshToken(string token);
    }
}
