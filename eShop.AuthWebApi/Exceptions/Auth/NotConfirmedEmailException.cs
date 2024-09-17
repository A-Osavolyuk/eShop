namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotConfirmedEmailException() : Exception("Cannot confirm your email address due to incorrect token or server error."), IInternalServerError;
}
