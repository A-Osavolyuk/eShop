using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotCreateBrandException() : Exception("Cannot create brand due to server error."), IInternalServerError;
}
