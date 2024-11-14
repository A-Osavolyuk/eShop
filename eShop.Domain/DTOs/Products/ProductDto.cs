using eShop.Domain.Entities.Product;
using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Products
{
    public record class ProductDto
    {
        public Guid Id { get; set; }
        public ProductTypes ProductType { get; set; } = ProductTypes.None;
        public string Article { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public BrandDto Brand { get; set; } = new BrandDto();
    }
}