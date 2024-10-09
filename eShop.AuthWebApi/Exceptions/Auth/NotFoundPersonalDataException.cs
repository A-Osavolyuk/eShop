namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class NotFoundPersonalDataException(string Id) : Exception(string.Format("Cannot find personal data of user with ID {0}.", Id)), INotFoundException;
} 
