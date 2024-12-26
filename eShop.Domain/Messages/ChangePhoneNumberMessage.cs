namespace eShop.Domain.Messages;

public class ChangePhoneNumberMessage : MessageBase
{
    public string Code { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}