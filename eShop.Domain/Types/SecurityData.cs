namespace eShop.Domain.Types;

public class SecurityData
{
    public bool TwoFactorAuthenticationState { get; set; }
    public DateTime PasswordUpdatedDate { get; set; }
}