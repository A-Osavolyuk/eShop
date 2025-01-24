namespace eShop.Domain.DTOs.Api.Product;

public record ShoesDto() : ProductDto
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public List<ProductSize> Size { get; set; } = new List<ProductSize>();
    public ProductAudience ProductAudience { get; set; } = ProductAudience.None;
}