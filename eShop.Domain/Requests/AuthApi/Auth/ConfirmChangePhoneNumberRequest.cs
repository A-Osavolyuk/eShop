namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ConfirmChangePhoneNumberRequest
{
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}