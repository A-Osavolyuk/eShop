namespace eShop.Domain.Models
{
    public class Favorites
    {
        public int ItemsCount { get; set; }
        public List<StoreItem> Products { get; set; } = new List<StoreItem>();

        public void Count()
        {
            ItemsCount = Products.Aggregate(0, (acc, v) => acc + v.Amount);
        }
    }
}
