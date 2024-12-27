namespace eShop.Domain.Messages.Email;

public class TwoFactorAuthenticationCodeEmail : EmailBase
{
    public string Code { get; set; } = string.Empty;
}