using eShop.Domain.Entities.Api.Auth;
using eShop.Domain.Responses.Api.Auth;

namespace eShop.Auth.Api.Services.Interfaces;

internal interface ITokenHandler
{
    public Task<TokenResponse> GenerateTokenAsync(AppUser user, List<string> roles, List<string> permissions);
    public TokenResponse? RefreshToken(string token);
}