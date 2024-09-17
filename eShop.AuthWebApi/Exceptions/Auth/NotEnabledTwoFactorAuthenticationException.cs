namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotEnabledTwoFactorAuthenticationException() : Exception("Your account has no 2FA enabled."), IBadRequestException;
}
