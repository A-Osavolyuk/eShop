using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public record class ClothingDTO : ProductDTO
    {
        public ClothingDTO() => this.ProductType = Categoty.Clothing;
        public HashSet<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
