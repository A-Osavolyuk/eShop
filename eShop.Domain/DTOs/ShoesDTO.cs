using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public class ShoesDTO : ProductDTO
    {
        public ShoesDTO() => this.ProductType = ProductType.Shoes;
        public int Size { get; set; }
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
