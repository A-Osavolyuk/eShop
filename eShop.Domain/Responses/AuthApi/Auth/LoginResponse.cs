using eShop.Domain.DTOs.AuthApi;

namespace eShop.Domain.Responses.AuthApi.Auth;

public record LoginResponse
{
    public UserDto User { get; set; } = null!;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool HasTwoFactorAuthentication { get; set; } = false;
}
