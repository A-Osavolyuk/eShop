namespace eShop.Domain.Entities.Api.Product;

public class ProductEntity
{
    public ProductEntity() => Article = GenerateArticle();
    
    public Guid Id { get; set; } = Guid.NewGuid();
    public ProductTypes ProductType { get; set; } = ProductTypes.None;
    public string Article { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductCurrency ProductCurrency { get; set; }
    public List<string> Images { get; set; } = new List<string>();

    public Guid BrandId { get; set; }
    public Guid SellerId { get; set; }
    public BrandEntity Brand { get; set; } = new BrandEntity();
    public SellerEntity Seller { get; set; } = new SellerEntity();

    public static string GenerateArticle() => new Random().NextInt64(100_000_000, 999_999_999_999).ToString();
}