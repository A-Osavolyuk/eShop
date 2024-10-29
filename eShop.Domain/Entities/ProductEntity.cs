using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class ProductEntity
    {
        public ProductEntity() => Article = GenerateArticle();

        public Guid Id { get; set; } = Guid.Empty;
        public Guid VariantId { get; set; } = Guid.Empty;
        public long Article { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Category Category { get; set; }
        public string Compound { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public Guid BrandId { get; set; } = Guid.Empty;
        public BrandEntity BrandEntity { get; set; } = null!;

        private long GenerateArticle()
        {
            var article = new Random().NextInt64(100_000_000, 100_000_000_000);
            return article;
        }
    }
}
