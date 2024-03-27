using eShop.Domain.Entities;

namespace eShop.Domain.DTOs.Requests
{
    public class CreateUpdateProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Guid SubcategoryId { get; set; }
        public Guid SupplierId { get; set; }
        public ProductDescription ProductDescription { get; set; } = null!;
    }
}
