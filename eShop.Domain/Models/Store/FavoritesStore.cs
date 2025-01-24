using eShop.Domain.Entities.Api.Cart;

namespace eShop.Domain.Models.Store;

public class FavoritesStore
{
    public Guid FavoritesId { get; set; }
    public int ItemsCount { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();

    public void Count()
    {
        ItemsCount = Items.Count();
    }
}