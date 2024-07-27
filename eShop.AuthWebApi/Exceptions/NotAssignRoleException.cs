namespace eShop.AuthWebApi.Exceptions
{
    public class NotAssignRoleException(string Message) : Exception(Message), IInternalServerError;
}
