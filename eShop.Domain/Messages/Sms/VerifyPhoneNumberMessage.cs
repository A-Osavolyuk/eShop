namespace eShop.Domain.Messages.Sms;

public class VerifyPhoneNumberMessage : SmsBase
{
    public string Code { get; set; } = string.Empty;
}