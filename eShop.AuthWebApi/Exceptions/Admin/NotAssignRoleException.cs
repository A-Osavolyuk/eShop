namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotAssignRoleException(string Message) : Exception(Message), IInternalServerError;
}
