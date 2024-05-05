using eShop.Domain.Enums;
using eShop.Domain.Interfaces;

namespace eShop.Domain.DTOs
{
    public class ClothingDTO : ProductDTO, ISizeable, IVariable
    {
        public ClothingDTO() => this.ProductType = ProductType.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
        public Guid VariantId { get; set; }
    }
}
