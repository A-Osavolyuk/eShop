﻿using eShop.Domain.Entities.Cart;
using MongoDB.Driver;

namespace eShop.CartApi.Extensions;

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