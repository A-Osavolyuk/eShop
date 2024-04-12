using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotFoundSupplierException : Exception, INotFoundException
    {
        public NotFoundSupplierException(Guid Id) : base($"Cannot find supplier with id: {Id}") { }
        public NotFoundSupplierException(string Name) : base($"Cannot find supplier with name: {Name}") { }
    }
}
