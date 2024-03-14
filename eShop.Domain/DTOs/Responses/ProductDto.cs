using eShop.Domain.Entities;

namespace eShop.Domain.DTOs.Responses
{
    public class ProductDto
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public Guid SubcategoryId { get; set; }
        public Guid SupplierId { get; set; }
        public ProductDescription ProductDescription { get; set; } = null!;
    }
}
