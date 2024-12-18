namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ResetPasswordRequest
{
    public string Email { get; set; } = string.Empty;
}