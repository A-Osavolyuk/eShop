namespace eShop.Domain.Models
{
    public class SecurityDataModel
    {
        public bool TwoFactorAuthenticationState { get; set; }
        public DateTime PasswordUpdatedDate { get; set; }
    }
}
