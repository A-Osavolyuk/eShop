using eShop.Domain.Responses.Auth;

namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface ITokenHandler
    {
        public Task<TokenResponse> GenerateTokenAsync(AppUser user, List<string> roles, List<string> permissions);
        public TokenResponse? ReuseToken(string token);
    }
}

