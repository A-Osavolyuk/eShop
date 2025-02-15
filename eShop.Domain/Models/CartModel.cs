﻿namespace eShop.Domain.Models;

public class CartModel : IIdentifiable<Guid>
{
    public Guid Id { get; set; }
    public int ItemsCount { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public void Count()
    {
        ItemsCount = Items.Aggregate(0, (acc, v) => acc + v.Amount);
    }
}