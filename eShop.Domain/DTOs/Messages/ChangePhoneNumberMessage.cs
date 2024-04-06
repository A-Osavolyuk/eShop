namespace eShop.Domain.DTOs.Messages
{
    public class ChangePhoneNumberMessage : MessageBase
    {
        public string Link { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
