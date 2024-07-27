namespace eShop.AuthWebApi.Exceptions
{
    public class NotChangedPhoneNumberException() : Exception("Cannot change your phone number due to server error."), IInternalServerError;
}
