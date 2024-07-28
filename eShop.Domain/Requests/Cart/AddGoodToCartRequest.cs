using eShop.Domain.Entities;

namespace eShop.Domain.DTOs.Requests.Cart
{
    public class AddGoodToCartRequest : RequestBase
    {
        public Guid CartId { get; set; }
        public Good Good { get; set; } = new();
    }
}
