using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Brands
{
    public class NotCreatedBrandException() : Exception("Cannot create brand due to server error."), IInternalServerError;
}
