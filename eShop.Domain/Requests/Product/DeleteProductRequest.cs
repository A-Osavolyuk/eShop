using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Product
{
    public record DeleteProductRequest : RequestBase
    {
        public Guid Id { get; set; }
    }
}
