namespace eShop.Domain.Requests.Product;

public record DeleteProductRequest()
{
    public Guid ProductId { get; set; }
}