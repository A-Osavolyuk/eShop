namespace eShop.Domain.Types;

public class SecurityData
{
    public bool TwoFactorAuthenticationState { get; set; }
    public DateTime PasswordUpdateDate { get; set; }
}