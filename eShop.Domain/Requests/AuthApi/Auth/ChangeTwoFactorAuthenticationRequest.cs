namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ChangeTwoFactorAuthenticationRequest
{
    public string Email { get; set; } = string.Empty;
}