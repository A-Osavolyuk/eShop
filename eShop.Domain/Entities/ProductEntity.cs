using System.Text.Json.Serialization;

namespace eShop.Domain.Entities
{
    public class ProductEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public Guid SupplierId { get; set; }
        public ProductDescription ProductDescription { get; set; } = null!;
        [JsonIgnore] public CategoryEntity Category { get; set; } = null!;
        [JsonIgnore] public SubcategoryEntity Subcategory { get; set; } = null!;
        [JsonIgnore] public SupplierEntity Supplier { get; set; } = null!;
    }
}
