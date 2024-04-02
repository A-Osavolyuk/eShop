namespace eShop.AuthWebApi.Exceptions
{
    public class UserAlreadyExistsException(string Email) : Exception($"User with email: {Email} already exists."), IBadRequestException;
}
