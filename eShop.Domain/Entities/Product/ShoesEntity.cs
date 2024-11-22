using eShop.Domain.Enums;

namespace eShop.Domain.Entities.Product;

public class ShoesEntity : ProductEntity
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public List<ProductSize> Size { get; set; } = new List<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
}