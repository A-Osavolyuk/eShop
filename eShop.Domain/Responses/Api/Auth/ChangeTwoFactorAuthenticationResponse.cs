namespace eShop.Domain.Responses.Api.Auth;

public class ChangeTwoFactorAuthenticationResponse
{
    public string Message { get; set; } = string.Empty;
    public bool TwoFactorAuthenticationState { get; set; } = false;
}