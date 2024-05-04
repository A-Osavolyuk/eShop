using eShop.Domain.Entities;
using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Requests
{
    public class UpdateProductRequestBase
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public string Compound { get; set; } = string.Empty;
        public Money Price { get; set; } = null!;
        public Guid SupplierId { get; set; } = Guid.Empty;
        public Guid BrandId { get; set; } = Guid.Empty;
    }
}
