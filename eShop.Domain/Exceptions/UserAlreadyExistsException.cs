namespace eShop.Domain.Exceptions
{
    public class UserAlreadyExistsException(string Email) : Exception($"User with email: {Email} already exists.");
}
