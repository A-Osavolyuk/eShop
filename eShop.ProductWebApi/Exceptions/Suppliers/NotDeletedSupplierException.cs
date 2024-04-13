using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Suppliers
{
    public class NotDeletedSupplierException() : Exception("Cannot delete supplier due to server error."), IInternalServerError;
}
