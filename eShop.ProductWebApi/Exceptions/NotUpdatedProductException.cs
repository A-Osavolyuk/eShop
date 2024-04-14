using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotUpdatedProductException(Guid Id) : Exception($"Cannot update product with id: {Id} due to server error."), IInternalServerError;
}
