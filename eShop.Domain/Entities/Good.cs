using eShop.Domain.Enums;

namespace eShop.Domain.Entities
{
    public class Good
    {
        public Guid GoodId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Article { get; set; }
        public int Amount { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public List<string> Images { get; set; } = new List<string>();
    }
}
