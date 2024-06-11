using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record class ProductDTO
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid VariantId { get; set; } = Guid.Empty;
        public long Article { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Categoty ProductType { get; set; }
        public string Compound { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Currency Currency { get; set; }
        public BrandDTO Brand { get; set; } = null!;
        public int ReviewsCount { get; set; }
        public int Rating { get; set; }
    }
}
