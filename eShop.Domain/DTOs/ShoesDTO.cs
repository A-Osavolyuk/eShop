using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public class ShoesDTO : ProductDTO
    {
        public ShoesDTO() => this.ProductType = ProductType.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
