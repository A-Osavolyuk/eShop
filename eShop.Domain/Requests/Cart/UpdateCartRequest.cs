using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Cart
{
    public record UpdateCartRequest : RequestBase
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public int ProductCount { get; set; }
        public List<Entities.Cart.Product> Products { get; set; } = new();
    }
}
