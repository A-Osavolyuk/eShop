namespace eShop.Domain.Messages.Email;

public class TwoFactorAuthenticationCodeMessage : EmailBase
{
    public string Code { get; set; } = string.Empty;
}