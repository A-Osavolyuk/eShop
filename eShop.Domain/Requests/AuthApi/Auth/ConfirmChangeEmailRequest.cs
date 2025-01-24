using eShop.Domain.Types;

namespace eShop.Domain.Requests.AuthApi.Auth;

public record class ConfirmChangeEmailRequest
{
    public string CurrentEmail { get; set; } = string.Empty;
    public string NewEmail { get; set; } = string.Empty;
    public CodeSet CodeSet { get; set; } = null!;
}