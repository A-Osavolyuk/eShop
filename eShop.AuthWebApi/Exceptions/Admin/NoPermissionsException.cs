namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NoPermissionsException() : Exception("Cannot get list of permisisons due to server error."), IInternalServerError;
}
