using eShop.Domain.Interfaces;

namespace eShop.CartWebApi.Exceptions
{
    public class NotFoundCartByUserIdException(Guid UserId) : Exception($"Cannot find cart by user id: {UserId}."), INotFoundException;
}
