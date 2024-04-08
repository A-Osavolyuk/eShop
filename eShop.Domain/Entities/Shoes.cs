using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Shoes : Product
    {
        public Shoes() => this.ProductType = ProductTypes.Shoes;
        public byte Size { get; set; }
        public Colors Color { get; set; }
        public Audience Gender { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
