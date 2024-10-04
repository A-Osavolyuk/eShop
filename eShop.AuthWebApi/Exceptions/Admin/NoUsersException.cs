namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NoUsersException() : Exception("Cannot get list of all users due to server error."), IInternalServerError;
}
