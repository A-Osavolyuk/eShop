namespace eShop.Domain.Messages.Email;

public class NewEmailVerificationMessage : EmailBase
{
    public string Code { get; set; } = string.Empty;
}