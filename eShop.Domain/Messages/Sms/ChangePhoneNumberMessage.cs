namespace eShop.Domain.Messages.Sms;

public class ChangePhoneNumberMessage : SmsBase
{
    public string Code { get; set; } = string.Empty;
}