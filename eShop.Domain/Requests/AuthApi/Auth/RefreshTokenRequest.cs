namespace eShop.Domain.Requests.AuthApi.Auth;

public record class RefreshTokenRequest
{
    public string Token { get; set; } = string.Empty;
}