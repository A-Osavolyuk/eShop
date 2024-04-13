using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotDeletedBrandException() : Exception("Cannot delete brand due to server error."), IInternalServerError;
}
