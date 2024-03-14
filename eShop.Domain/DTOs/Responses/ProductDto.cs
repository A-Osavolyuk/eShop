using eShop.Domain.Entities;

namespace eShop.Domain.DTOs.Responses
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductDescription ProductDescription { get; set; } = null!;
        public SubcategoryDto Subcategory { get; set; } = null!;
        public SupplierDto Supplier { get; set; } = null!;
    }
}
