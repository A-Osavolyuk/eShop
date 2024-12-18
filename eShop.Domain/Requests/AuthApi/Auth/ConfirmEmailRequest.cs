namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ConfirmEmailRequest
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}