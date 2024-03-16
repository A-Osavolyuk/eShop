namespace eShop.Domain.Exceptions.Auth
{
    public class NotFoundUserByEmailException(string Email) : Exception($"Cannot find user with email: {Email}");
}
