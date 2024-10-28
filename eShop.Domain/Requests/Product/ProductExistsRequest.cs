using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Product;

public record ProductExistsRequest : RequestBase
{
    public Guid ProductId { get; set; }
}