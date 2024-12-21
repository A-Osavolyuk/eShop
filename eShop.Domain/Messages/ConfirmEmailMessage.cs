namespace eShop.Domain.Messages;

public class ConfirmEmailMessage : MessageBase
{
    public int Code { get; init; }
}