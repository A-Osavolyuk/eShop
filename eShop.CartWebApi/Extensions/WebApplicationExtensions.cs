using eShop.Domain.Entities.Cart;
using MongoDB.Driver;

namespace eShop.CartWebApi.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        var cartCollection = database.GetCollection<CartEntity>("Carts");
        var favoritesCollection = database.GetCollection<FavoritesEntity>("Favorites");
        
        await cartCollection.InsertOneAsync(new CartEntity()
        {
            CartId = Guid.NewGuid(), 
            UserId = Guid.NewGuid(),
            ItemsCount = 0,
            Items = new List<CartItem>(),
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        });

        await favoritesCollection.InsertOneAsync(new FavoritesEntity()
        {
            FavoritesId = Guid.NewGuid(), 
            UserId = Guid.NewGuid(),
            ItemsCount = 0,
            Items = new List<FavoritesItem>(),
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        });
    }
}