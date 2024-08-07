using eShop.Domain.Interfaces;

namespace eShop.CartWebApi.Exceptions
{
    public class NotFoundCartByUserIdException(Guid UserId) : Exception(string.Format("Cannot find cart by user id: {0}.", UserId)), INotFoundException;
}
