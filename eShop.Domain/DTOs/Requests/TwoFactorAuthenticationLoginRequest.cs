namespace eShop.Domain.DTOs.Requests
{
    public class TwoFactorAuthenticationLoginRequest
    {
        public string Code { get; set; } = string.Empty;
    }
}
