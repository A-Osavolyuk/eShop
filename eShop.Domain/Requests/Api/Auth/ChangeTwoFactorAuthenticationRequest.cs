namespace eShop.Domain.Requests.Api.Auth;

public record class ChangeTwoFactorAuthenticationRequest
{
    public string Email { get; set; } = string.Empty;
}