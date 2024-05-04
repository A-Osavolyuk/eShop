using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Clothing : Product
    {
        public Clothing() => ProductType = ProductType.Clothing;
        public List<ProductSize> Sizes { get; set; } = new();
        public List<ProductColor> Colors { get; set; } = new();
        public Audience Audience { get; set; }
    }
}
