using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public class ClothingDTO : ProductDTO
    {
        public ClothingDTO() => this.ProductType = ProductType.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
