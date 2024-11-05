using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities.Product;
using eShop.Domain.Enums;

namespace eShop.Domain.Requests.Product;

public record UpdateProductRequest() : RequestBase
{
    public Guid Id { get; set; }
    public ProductTypes ProductType { get; set; } = ProductTypes.None;
    public string Article { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Price Price { get; set; } =  new Price { Amount = 0, Currency = Currency.None };
    public List<string> Images { get; set; } = new List<string>();
    public BrandEntity Brand { get; set; } = new BrandEntity();
    
    public ProductColor Color { get; set; } = ProductColor.None;
    public HashSet<ProductSize> Size { get; set; } = new HashSet<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
}