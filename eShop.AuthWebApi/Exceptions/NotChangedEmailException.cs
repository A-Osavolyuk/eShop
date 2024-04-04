namespace eShop.AuthWebApi.Exceptions
{
    public class NotChangedEmailException() : Exception("Cannot change your email address due to server error."), IInternalServerError;
}
