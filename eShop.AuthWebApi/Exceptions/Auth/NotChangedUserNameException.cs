namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotChangedUserNameException() : Exception("Cannot change user name due to server error."), IInternalServerError;
}
