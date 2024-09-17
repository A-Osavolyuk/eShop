namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotResetPasswordException() : Exception("Cannot reset your password due to incorrect reset token or server error."), IInternalServerError;
}
