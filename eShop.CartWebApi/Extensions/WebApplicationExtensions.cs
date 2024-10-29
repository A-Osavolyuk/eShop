using eShop.Domain.Entities.Cart;
using MongoDB.Driver;

namespace eShop.CartWebApi.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        var collection = database.GetCollection<CartEntity>("Carts");
        await collection.InsertOneAsync(new CartEntity()
        {
            CartId = Guid.NewGuid(), 
            UserId = Guid.NewGuid(),
            ItemsCount = 0,
            Items = new List<CartItem>(),
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        });
    }
}