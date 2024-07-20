using eShop.Domain.Entities;

namespace eShop.Domain.DTOs
{
    public class CartDTO
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public int GoodsCount { get; set; }
        public List<GoodDTO> Goods { get; set; } = new();
    }
}
