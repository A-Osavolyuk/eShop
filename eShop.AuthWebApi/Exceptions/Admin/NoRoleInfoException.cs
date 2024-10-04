namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NoRoleInfoException() : Exception("Cannot find role information due to server error."), IInternalServerError;
}
