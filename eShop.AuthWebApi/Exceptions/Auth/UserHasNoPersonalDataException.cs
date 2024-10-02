namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class UserHasNoPersonalDataException(string UserId) : 
        Exception(string.Format("User with ID {0} has no personal data yet. Please add personal data first", UserId)), INotFoundException;
}
