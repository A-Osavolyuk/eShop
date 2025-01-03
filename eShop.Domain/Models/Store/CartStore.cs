﻿using eShop.Domain.Entities.CartApi;

namespace eShop.Domain.Models.Store;

public class CartStore
{
    public Guid CartId { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public void Count()
    {
        ItemsCount = Items.Aggregate(0, (acc, v) => acc + v.Amount);
    }
}