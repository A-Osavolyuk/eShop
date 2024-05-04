using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public class ClothingDTO : ProductDTO
    {
        public ClothingDTO() => this.ProductType = ProductType.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public List<ProductColor> Colors { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
