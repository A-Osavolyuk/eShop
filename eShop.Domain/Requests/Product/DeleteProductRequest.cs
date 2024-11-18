namespace eShop.Domain.Requests.Product;

public record DeleteProductRequest() : RequestBase()
{
    public Guid ProductId { get; set; }
}