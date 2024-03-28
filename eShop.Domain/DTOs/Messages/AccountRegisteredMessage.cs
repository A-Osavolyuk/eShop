namespace eShop.Domain.DTOs.Messages
{
    public class AccountRegisteredMessage
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
