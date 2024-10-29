using eShop.Domain.Entities.Cart;
using MongoDB.Driver;
using Cart = eShop.Domain.Entities.Cart.Cart;

namespace eShop.CartWebApi.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
        var collection = database.GetCollection<Cart>("Carts");
        await collection.InsertOneAsync(new Cart()
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