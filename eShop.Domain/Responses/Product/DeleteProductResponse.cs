namespace eShop.Domain.Responses.Product;

public class DeleteProductResponse
{
    public string Message { get; set; } = string.Empty;
    public bool IsSucceeded { get; set; }
}