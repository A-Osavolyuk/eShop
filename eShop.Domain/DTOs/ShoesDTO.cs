using eShop.Domain.Enums;

namespace eShop.Domain.DTOs
{
    public class ShoesDTO : ProductDTO
    {
        public ShoesDTO() => this.ProductType = ProductType.Shoes;
        public byte Size { get; set; }
        public Colors Color { get; set; }
        public Audience Audience { get; set; }
    }
}
