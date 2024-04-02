namespace eShop.AuthWebApi.Exceptions
{
    public class InvalidTwoFactorAuthenticationCodeException() : Exception("Invalid 2FA code."), IBadRequestException;
}
