using eShop.Domain.Entities;

namespace eShop.Domain.DTOs.Requests.Cart
{
    public record class UpdateCartRequest : RequestBase
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public int GoodsCount { get; set; }
        public List<GoodDTO> Goods { get; set; } = new();
    }
}
