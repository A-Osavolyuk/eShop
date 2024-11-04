using eShop.Domain.Enums;

namespace eShop.Domain.Entities.Product;

public class ShoesEntity : ProductEntity
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public HashSet<ProductSize> Size { get; set; } = new HashSet<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
}