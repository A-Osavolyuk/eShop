namespace eShop.Domain.Requests.ProductApi.Brand;

public record CreateBrandRequest
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}