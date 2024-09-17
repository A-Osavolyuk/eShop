namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotDeletedRoleException(IdentityError error) :
        Exception(string.Format("Cannot delete role due to server error with message: {0}", error.Description)), IInternalServerError;
}
