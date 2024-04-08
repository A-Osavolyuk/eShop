namespace eShop.Domain.DTOs.Messages
{
    public class TwoFactorAuthenticationCodeMessage : MessageBase
    {
        public string Code { get; set; } = string.Empty;
    }
}
