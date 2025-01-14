using eShop.Cart.Api.Data;

namespace eShop.Cart.Api.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<DbClient>();
        var cartCollection = client.GetCollection<CartEntity>("Carts");
        var favoritesCollection = client.GetCollection<FavoritesEntity>("Favorites");
        
        await cartCollection.InsertOneAsync(new CartEntity()
        {
            CartId = Guid.NewGuid(), 
            UserId = Guid.Parse("abb9d2ed-c3d2-4df9-ba88-eab018b95bc3"),
            ItemsCount = 0,
            Items = new List<CartItem>(),
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        });

        await favoritesCollection.InsertOneAsync(new FavoritesEntity()
        {
            FavoritesId = Guid.NewGuid(), 
            UserId = Guid.Parse("abb9d2ed-c3d2-4df9-ba88-eab018b95bc3"),
            ItemsCount = 0,
            Items = new List<FavoritesItem>(),
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        });
    }
}