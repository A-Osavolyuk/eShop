namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotRemovedRoleException(string Message) : Exception(Message), IInternalServerError;
}
