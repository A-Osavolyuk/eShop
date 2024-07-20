using eShop.Domain.Interfaces;

namespace eShop.CartWebApi.Exceptions
{
    public class NotFoundCartException(Guid Id) : Exception($"Cannot find cart with id: {Id}"), INotFoundException;
}
