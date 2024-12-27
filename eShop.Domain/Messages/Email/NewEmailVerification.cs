namespace eShop.Domain.Messages.Email;

public class NewEmailVerification : EmailBase
{
    public string Code { get; set; } = string.Empty;
}