using eShop.Domain.Enums;
using eShop.Domain.Interfaces;

namespace eShop.Domain.DTOs
{
    public class ShoesDTO : ProductDTO, ISizeable, IVariable
    {
        public ShoesDTO() => this.ProductType = ProductType.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
        public Guid VariantId { get; set; }
    }
}
