namespace eShop.Domain.DTOs.Requests.Auth
{
    public record class TwoFactorAuthenticationLoginRequest : RequestBase
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
