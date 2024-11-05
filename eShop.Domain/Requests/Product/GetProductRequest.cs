using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Product;

public record GetProductRequest() : RequestBase
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductArticle { get; set; } = string.Empty;
}