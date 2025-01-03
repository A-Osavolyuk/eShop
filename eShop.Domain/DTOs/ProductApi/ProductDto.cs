namespace eShop.Domain.DTOs.ProductApi;

public record class ProductDto
{
    public Guid Id { get; set; }
    public ProductTypes ProductType { get; set; } = ProductTypes.None;
    public string Article { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductCurrency ProductCurrency { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public BrandDto Brand { get; set; } = new BrandDto();
    public SellerDto Seller { get; set; } = new SellerDto();
}