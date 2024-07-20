using eShop.Domain.Interfaces;

namespace eShop.CartWebApi.Exceptions
{
    public class NotFoundUserException(Guid UserId) : Exception($"Cannot create cart with non-existent user id. Cannot find user with id: {UserId}"), INotFoundException;
}
