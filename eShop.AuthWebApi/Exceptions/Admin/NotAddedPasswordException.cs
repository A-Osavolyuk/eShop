namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotAddedPasswordException() : Exception("Cannot added password for user account due to server error."), IInternalServerError;
}
