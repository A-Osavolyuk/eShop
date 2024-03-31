namespace eShop.Domain.DTOs.Messages
{
    public class ChangeEmailMessage : MessageBase
    { 
        public string Link { get; set; } = string.Empty;
    }
}
