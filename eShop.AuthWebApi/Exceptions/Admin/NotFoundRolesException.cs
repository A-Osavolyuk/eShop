namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotFoundRolesException() : Exception("Cannot find any role."), INotFoundException;
}
