using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid VariantId { get; set; } = Guid.Empty;
        public long Article { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public string Compound { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public SupplierDTO Supplier { get; set; } = null!;
        public BrandDTO Brand { get; set; } = null!;
    }
}
