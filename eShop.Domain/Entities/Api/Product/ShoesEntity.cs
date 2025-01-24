namespace eShop.Domain.Entities.Api.Product;

public class ShoesEntity : ProductEntity
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public List<ProductSize> Size { get; set; } = new List<ProductSize>();
    public ProductAudience ProductAudience { get; set; } = ProductAudience.None;
}