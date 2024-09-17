namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotChangedTwoFactorAuthenticationException() : Exception("Cannot change 2FA state for your account due to server error."), IInternalServerError;
}