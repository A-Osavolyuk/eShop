using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Brands
{
    public class NotDeletedBrandException(Guid Id) : Exception($"Cannot delete brand with id: {Id} due to server error."), IInternalServerError;
}
