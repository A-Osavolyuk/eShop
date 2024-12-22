namespace eShop.Domain.Messages;

public class ConfirmEmailMessage : MessageBase
{
    public string Code { get; init; }
}