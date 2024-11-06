using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities.Product;
using eShop.Domain.Enums;

namespace eShop.Domain.Requests.Product;

public record CreateProductRequest : RequestBase
{
    public ProductTypes ProductType { get; set; } = ProductTypes.None;
    public string Article { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Currency Currency { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public BrandEntity Brand { get; set; } = new BrandEntity();
    public ProductColor Color { get; set; } = ProductColor.None;
    public IEnumerable<ProductSize> Size { get; set; } = Enumerable.Empty<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
};