using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Suppliers
{
    public class NotUpdatedSupplierException() : Exception("Cannot update supplier due to server error."), IInternalServerError;
}
