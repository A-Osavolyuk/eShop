namespace eShop.Domain.Messages;

public class ChangeEmailMessage : MessageBase
{
    public string Link { get; set; } = string.Empty;
    public string NewEmail { get; set; } = string.Empty;
}