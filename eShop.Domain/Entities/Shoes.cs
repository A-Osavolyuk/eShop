using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Shoes : Product
    {
        public Shoes() => this.ProductType = ProductType.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public List<ProductColor> Colors { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
