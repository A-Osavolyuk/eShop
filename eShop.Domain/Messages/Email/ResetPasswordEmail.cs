namespace eShop.Domain.Messages.Email;

public class ResetPasswordEmail : EmailBase
{
    public string Code { get; set; } = string.Empty;
}