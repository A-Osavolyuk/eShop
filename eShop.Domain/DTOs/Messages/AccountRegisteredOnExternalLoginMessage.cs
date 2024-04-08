namespace eShop.Domain.DTOs.Messages
{
    public class AccountRegisteredOnExternalLoginMessage : MessageBase
    {
        public string TempPassword { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;
    }
}
