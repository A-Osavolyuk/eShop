namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotUnlockedAccountException() : Exception("Cannot unlock user account due to server error"), IInternalServerError;
}
