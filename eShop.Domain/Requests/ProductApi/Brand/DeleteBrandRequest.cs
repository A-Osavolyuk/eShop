namespace eShop.Domain.Requests.ProductApi.Brand;

public record DeleteBrandRequest
{
    public Guid Id { get; set; }
}