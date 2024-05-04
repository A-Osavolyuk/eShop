using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Shoes : Product
    {
        public Shoes() => this.ProductType = ProductType.Shoes;
        public int Size { get; set; }
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
