namespace eShop.AuthWebApi.Exceptions.Auth
{
    public class UserAlreadyExistsException(string Email) : Exception($"User with email: {Email} already exists."), IBadRequestException;
}
