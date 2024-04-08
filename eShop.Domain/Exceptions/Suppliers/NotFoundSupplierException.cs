using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Suppliers
{
    public class NotFoundSupplierException : Exception, INotFoundException
    {
        public NotFoundSupplierException(Guid id) : base($"Cannot find Supplier with id: {id}.") { }
        public NotFoundSupplierException(string name) : base($"Cannot find Supplier with name: {name}.") { }
    }
}
