namespace eShop.Domain.Messages;

public class EmailVerificationMessage : MessageBase
{
    public string Code { get; init; }
}