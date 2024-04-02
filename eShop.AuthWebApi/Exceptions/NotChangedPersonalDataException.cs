namespace eShop.AuthWebApi.Exceptions
{
    public class NotChangedPersonalDataException() : Exception("Cannot change user`s personal data."), IInternalServerError;
}
