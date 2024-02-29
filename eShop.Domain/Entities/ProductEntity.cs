namespace eShop.Domain.Entities
{
    public class ProductEntity
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid SubcategoryId { get; set; }
        public Guid SupplierId { get; set; }
        public ProductDescription ProductDescription { get; set; } = null!;
        public SubcategoryEntity Subcategory { get; set; } = null!;
        public SupplierEntity Supplier { get; set; } = null!;
    }
}
