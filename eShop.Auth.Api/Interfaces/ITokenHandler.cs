namespace eShop.Auth.Api.Interfaces;

internal interface ITokenHandler
{
    public Task<TokenResponse> GenerateTokenAsync(AppUser user, List<string> roles, List<string> permissions);
    public TokenResponse? RefreshToken(string token);
}