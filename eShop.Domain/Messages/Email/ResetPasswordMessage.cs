namespace eShop.Domain.Messages.Email;

public class ResetPasswordMessage : EmailBase
{
    public string Code { get; set; } = string.Empty;
}