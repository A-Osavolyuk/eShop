namespace eShop.Domain.DTOs.Messages
{
    public class ConfirmEmailMessage : MessageBase
    {
        public string Link { get; set; } = string.Empty;
    }
}
