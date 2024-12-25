namespace eShop.Domain.Requests.AuthApi.Auth;

public class ResendEmailVerificationCodeRequest
{
    public string Email { get; set; } = string.Empty;
}