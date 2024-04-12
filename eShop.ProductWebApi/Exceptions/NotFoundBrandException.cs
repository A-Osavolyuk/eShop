using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotFoundBrandException : Exception, INotFoundException
    {
        public NotFoundBrandException(Guid Id) : base($"Cannot find brand with id: {Id}") { }
        public NotFoundBrandException(string Name) : base($"Cannot find brand with name: {Name}") { }
    }
}
