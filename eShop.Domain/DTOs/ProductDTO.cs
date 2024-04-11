using eShop.Domain.Entities;
using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public Money Price { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;
        public Brand Brand { get; set; } = null!;
    }
}
