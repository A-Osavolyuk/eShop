namespace eShop.Domain.Exceptions.Suppliers
{
    public class NotDeletedSupplierException(Guid id) : Exception($"Supplier with id: {id} was not deleted due to DB error.");
}
