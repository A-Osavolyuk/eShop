namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotChangedPasswordException(string message) : Exception(message), IInternalServerError;
}
