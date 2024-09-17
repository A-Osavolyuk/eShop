namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class InvalidTwoFactorAuthenticationCodeException() : Exception("Invalid 2FA code."), IBadRequestException;
}
