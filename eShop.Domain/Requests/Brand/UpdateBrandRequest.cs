using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Brand
{
    public record UpdateBrandRequest : RequestBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
