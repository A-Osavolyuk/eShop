namespace eShop.Domain.Requests.AuthApi.Auth;

public class ForgotPasswordRequest
{
    public string Email { get; set; } = string.Empty;
}