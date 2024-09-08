namespace eShop.AuthWebApi.Exceptions
{
    public class NotCreatedRoleException(string Message) : Exception(string.Format("Cannot create role due to server error with message: {0}", Message)), IInternalServerError;
}
