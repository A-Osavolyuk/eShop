namespace eShop.Domain.DTOs.Requests.Auth
{
    public class TwoFactorAuthenticationLoginRequest
    {
        public string Code { get; set; } = string.Empty;
    }
}
