namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotChangedPersonalDataException() : Exception("Cannot change user`s personal data."), IInternalServerError;
}
