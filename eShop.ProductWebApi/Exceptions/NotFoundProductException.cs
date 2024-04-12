using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotFoundProductException : Exception, INotFoundException
    {
        public NotFoundProductException(Guid Id) : base($"Cannot find product with id: {Id}") { }
        public NotFoundProductException(string Name) : base($"Cannot find product with name: {Name}") { }
    }
}
