using eShop.Domain.Enums;
using eShop.Domain.Interfaces;

namespace eShop.Domain.Entities
{
    public class Clothing : Product, ISizeable, IVariable
    {
        public Clothing() => ProductType = ProductType.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; } = new();
        public Audience Audience { get; set; }
        public Guid VariantId { get; set; }
    }
}
