namespace eShop.Domain.DTOs.Messages
{
    public class AccountRegisteredOnExternalLoginMessage
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string TempPassword { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;
    }
}
