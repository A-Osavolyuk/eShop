using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotFoundClothingException : Exception, INotFoundException
    {
        public NotFoundClothingException(Guid Id) : base($"Cannot find clothing with id: {Id}") { }
        public NotFoundClothingException(string Name) : base($"Cannot find clothing with name: {Name}") { }
    }
}
