using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Suppliers
{
    public class NotDeletedSupplierException(Guid Id) : Exception($"Cannot delete supplier with id {Id} due to server error."), IInternalServerError;
}
