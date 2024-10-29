using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class ShoesEntity : ProductEntity
    {
        public ShoesEntity() : base() => Category = Category.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public ProductColor Color { get; set; }
        public Audience Audience { get; set; }
    }
}
