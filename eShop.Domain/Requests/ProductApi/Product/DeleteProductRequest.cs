namespace eShop.Domain.Requests.ProductApi.Product;

public record DeleteProductRequest()
{
    public Guid ProductId { get; set; }
}