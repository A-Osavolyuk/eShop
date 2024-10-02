using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Brand
{
    public record DeleteBrandRequest : RequestBase
    {
        public Guid Id { get; set; }
    }
}
