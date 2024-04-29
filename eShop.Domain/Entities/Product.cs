using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public long Article { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public Money Price { get; set; } = null!;
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public Brand Brand { get; set; } = null!;
    }
}
