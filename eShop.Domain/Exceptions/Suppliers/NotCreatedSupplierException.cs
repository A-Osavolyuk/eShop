using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Suppliers
{
    public class NotCreatedSupplierException() : Exception("Supplier was not created due to DB error."), IInternalServerError;
}
