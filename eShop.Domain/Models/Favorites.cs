using eShop.Domain.Entities.Cart;

namespace eShop.Domain.Models
{
    public class Favorites
    {
        public Guid FavoritesId { get; set; }
        public int ItemsCount { get; set; }
        public List<FavoritesItem> Products { get; set; } = new List<FavoritesItem>();

        public void Count()
        {
            ItemsCount = Products.Count();
        }
    }
}
