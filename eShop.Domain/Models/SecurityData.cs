namespace eShop.Domain.Models
{
    public class SecurityData
    {
        public bool TwoFactorAuthenticationState { get; set; }
        public DateTime PasswordUpdatedDate { get; set; }
    }
}
