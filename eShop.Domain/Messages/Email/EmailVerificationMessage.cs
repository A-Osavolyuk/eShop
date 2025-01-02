namespace eShop.Domain.Messages.Email;

public class EmailVerificationMessage : EmailBase
{
    public string Code { get; init; } = string.Empty;
}