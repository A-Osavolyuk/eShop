namespace eShop.AuthWebApi.Exceptions
{
    public class NotChangedUserNameException() : Exception("Cannot change user name due to server error."), IInternalServerError;
}
