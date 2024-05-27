using eShop.Domain.Interfaces;

namespace eShop.ProductWebApi.Exceptions
{
    public class NotFoundProductGroupException : Exception, INotFoundException
    {
        public NotFoundProductGroupException(Guid VariantId) : base($"Cannot find product group with variant id: {VariantId}") { }
    }
}
