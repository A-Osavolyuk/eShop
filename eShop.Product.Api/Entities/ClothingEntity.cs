namespace eShop.Product.Api.Entities;

public class ClothingEntity : ProductEntity
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public List<ProductSize> Size { get; set; } = new List<ProductSize>();
    public ProductAudience ProductAudience { get; set; } = ProductAudience.None;
}