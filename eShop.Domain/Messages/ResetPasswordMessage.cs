namespace eShop.Domain.Messages;

public class ResetPasswordMessage : MessageBase
{
    public string Link { get; set; } = string.Empty;
}