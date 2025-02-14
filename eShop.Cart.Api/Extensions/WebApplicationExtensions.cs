﻿using eShop.Cart.Api.Entities;
using eShop.Domain.Types;
using Scalar.AspNetCore;

namespace eShop.Cart.Api.Extensions;

public static class WebApplicationExtensions
{
    public static async Task MapApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
            await app.SeedDataAsync();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseExceptionHandler();
        app.MapGrpcService<CartServer>();
    }

    private static async Task SeedDataAsync(this WebApplication app)
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