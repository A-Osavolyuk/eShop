using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Cloth : Product
    {
        public Cloth() => this.ProductType = ProductTypes.Cloth;
        public byte Size { get; set; }
        public Colors Color { get; set; }
        public Audience Gender { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
