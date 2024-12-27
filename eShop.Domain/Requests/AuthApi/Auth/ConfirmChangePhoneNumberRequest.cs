namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ConfirmChangePhoneNumberRequest
{
    public string CurrentPhoneNumber { get; set; } = string.Empty;
    public string NewPhoneNumber { get; set; } = string.Empty;
    public CodeSet CodeSet { get; set; } = null!;
}