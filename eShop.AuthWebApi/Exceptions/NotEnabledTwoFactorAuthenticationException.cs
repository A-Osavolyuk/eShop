namespace eShop.AuthWebApi.Exceptions
{
    public class NotEnabledTwoFactorAuthenticationException() : Exception("Your account has no 2FA enabled.");
}
