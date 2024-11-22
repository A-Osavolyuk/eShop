using eShop.Domain.Enums;

namespace eShop.Domain.DTOs.Products;

public record ShoesDto() : ProductDto
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public List<ProductSize> Size { get; set; } = new List<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
}