namespace eShop.Domain.Messages;

public class ChangeEmailMessage : MessageBase
{
    public string Code { get; set; } = string.Empty;
    public string NewEmail { get; set; } = string.Empty;
}