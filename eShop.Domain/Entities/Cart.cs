namespace eShop.Domain.Entities
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public int GoodsCount { get; set; }
        public List<Good> Goods { get; set; } = new();
    }
}
