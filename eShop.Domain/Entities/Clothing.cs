using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Clothing : Product
    {
        public Clothing() => this.ProductType = ProductTypes.Cloth;
        public byte Size { get; set; }
        public Colors Color { get; set; }
        public Audience Audience { get; set; }
    }
}
