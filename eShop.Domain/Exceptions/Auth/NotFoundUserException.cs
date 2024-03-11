namespace eShop.Domain.Exceptions.Auth
{
    public class NotFoundUserException(string Id) : Exception($"Cannot find user with id: {Id}");
}
