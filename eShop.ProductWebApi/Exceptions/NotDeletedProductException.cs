using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotDeletedProductException(Guid Id) : Exception($"Cannot delete product with id: {Id} due to server error."), IInternalServerError;
}
