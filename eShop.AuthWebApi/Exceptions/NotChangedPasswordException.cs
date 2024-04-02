namespace eShop.AuthWebApi.Exceptions
{
    public class NotChangedPasswordException(string message) : Exception(message), IInternalServerError;
}
