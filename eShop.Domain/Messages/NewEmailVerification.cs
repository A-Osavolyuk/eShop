namespace eShop.Domain.Messages;

public class NewEmailVerification : MessageBase
{
    public string Code { get; set; } = string.Empty;
}