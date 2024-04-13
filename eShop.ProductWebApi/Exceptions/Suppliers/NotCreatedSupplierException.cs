using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Suppliers
{
    public class NotCreatedSupplierException() : Exception("Cannot create supplier due to server error."), IInternalServerError;
}
