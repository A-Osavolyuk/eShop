using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Cart;

public record GetCartRequest() : RequestBase
{
    public Guid UserId { get; set; }
}