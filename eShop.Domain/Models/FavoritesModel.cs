using eShop.Domain.Types;

namespace eShop.Domain.Models;

public class FavoritesModel
{
    public Guid FavoritesId { get; set; }
    public int ItemsCount { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();

    public void Count()
    {
        ItemsCount = Items.Count();
    }
}