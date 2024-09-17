namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotChangedEmailException() : Exception("Cannot change your email address due to server error."), IInternalServerError;
}
