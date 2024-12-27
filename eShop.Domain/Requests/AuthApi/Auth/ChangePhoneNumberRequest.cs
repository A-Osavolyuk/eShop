namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ChangePhoneNumberRequest
{
    public string CurrentPhoneNumber { get; set; } = string.Empty;
    public string NewPhoneNumber { get; set; } = string.Empty;
}