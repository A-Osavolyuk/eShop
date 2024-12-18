using eShop.Domain.Entities.AuthApi;
using eShop.Domain.Responses.AuthApi.Auth;

namespace eShop.AuthApi.Services.Interfaces;

internal interface ITokenHandler
{
    public Task<TokenResponse> GenerateTokenAsync(AppUser user, List<string> roles, List<string> permissions);
    public TokenResponse? ReuseToken(string token);
}