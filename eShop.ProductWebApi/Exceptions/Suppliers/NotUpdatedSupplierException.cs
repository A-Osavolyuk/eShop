using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions.Suppliers
{
    public class NotUpdatedSupplierException(Guid Id) : Exception($"Cannot update supplier with id: {Id} due to server error."), IInternalServerError;
}
