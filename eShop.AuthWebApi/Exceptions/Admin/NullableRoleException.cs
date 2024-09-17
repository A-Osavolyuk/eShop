namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NullableRoleException() : Exception("Cannot get list of roles due to server error. Nullable role."), IInternalServerError;
}
