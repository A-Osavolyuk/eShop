using eShop.Domain.Entities;

namespace eShop.Domain.DTOs.Requests.Cart
{
    public class UpdateCartRequest : RequestBase
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public int GoodsCount { get; set; }
        public List<Good> Goods { get; set; } = new();
    }
}
