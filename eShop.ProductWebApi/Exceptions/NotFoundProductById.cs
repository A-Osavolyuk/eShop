using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotFoundProductById(Guid Id) : Exception($"Cannot find product with id: {Id}."), INotFoundException;
}
