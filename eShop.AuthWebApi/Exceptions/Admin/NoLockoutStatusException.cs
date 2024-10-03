namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NoLockoutStatusException() : Exception("Cannot get lockout status due to server error."), IInternalServerError;
}
