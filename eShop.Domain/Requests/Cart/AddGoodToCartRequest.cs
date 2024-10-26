using eShop.Domain.Entities;

namespace eShop.Domain.DTOs.Requests.Cart
{
    public record class AddGoodToCartRequest : RequestBase
    {
        public Guid CartId { get; set; }
        public CartItem CartItem { get; set; } = new();
    }
}
