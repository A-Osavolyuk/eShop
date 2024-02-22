using System.Text.Json.Serialization;

namespace eShop.Domain.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        [JsonIgnore] public Guid CategoryId { get; set; }
        [JsonIgnore] public Guid SupplierId { get; set; }
        public ProductDescription ProductDescription { get; set; } = null!;
        public CategoryEntity Category { get; set; } = null!;
        public Supplier Supplier { get; set; } = null!;
    }
}
