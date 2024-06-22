using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Shoes : Product
    {
        public Shoes() : base() => Category = Categoty.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
