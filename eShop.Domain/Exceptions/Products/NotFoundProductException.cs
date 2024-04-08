using eShop.Domain.Interfaces;

namespace eShop.Domain.Exceptions.Products
{
    public class NotFoundProductException : Exception, INotFoundException
    {
        public NotFoundProductException(Guid id) : base($"Cannot find Product with id: {id}.") { }
        public NotFoundProductException(string name) : base($"Cannot find Product with name: {name}.") { }
    }
}
