namespace eShop.Domain.Messages;

public class TwoFactorAuthenticationCodeMessage : MessageBase
{
    public string Code { get; set; } = string.Empty;
}