using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Brands
{
    public class NotDeletedBrandException() : Exception("Cannot delete brand due to server error."), IInternalServerError;
}
