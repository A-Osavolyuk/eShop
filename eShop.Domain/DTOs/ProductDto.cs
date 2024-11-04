using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record class ProductDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid VariantId { get; set; } = Guid.Empty;
        public long Article { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Category Category { get; set; }
        public string Compound { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Currency Currency { get; set; }
        public BrandDTO Brand { get; set; } = null!;
        public int CommentsCount { get; set; }
        public int Rating { get; set; }
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
