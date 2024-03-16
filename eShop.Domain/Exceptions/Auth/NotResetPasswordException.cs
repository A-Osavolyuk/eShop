namespace eShop.Domain.Exceptions.Auth
{
    public class NotResetPasswordException() : Exception("Cannot reset your password due to incorrect reset token or server error.");
}
