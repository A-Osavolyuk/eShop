namespace eShop.Domain.Messages.Email;

public class EmailVerificationEmail : EmailBase
{
    public string Code { get; init; } = string.Empty;
}