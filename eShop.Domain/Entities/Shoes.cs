using eShop.Domain.Enums;
using eShop.Domain.Interfaces;

namespace eShop.Domain.Entities
{
    public class Shoes : Product
    {
        public Shoes() => this.ProductType = ProductType.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
