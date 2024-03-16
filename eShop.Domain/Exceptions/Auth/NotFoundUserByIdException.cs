namespace eShop.Domain.Exceptions.Auth
{
    public class NotFoundUserByIdException(string Id) : Exception($"Cannot find user with id: {Id}");
}
