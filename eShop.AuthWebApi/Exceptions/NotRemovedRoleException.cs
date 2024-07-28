namespace eShop.AuthWebApi.Exceptions
{
    public class NotRemovedRoleException(string Message) : Exception(Message), IInternalServerError;
}
