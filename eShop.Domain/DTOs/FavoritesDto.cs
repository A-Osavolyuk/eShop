﻿using eShop.Domain.Entities.Cart;

namespace eShop.Domain.DTOs;

public class FavoritesDto
{
    public Guid FavoritesId { get; set; }
    public int ItemsCount { get; set; }
    public List<FavoritesItem> Items { get; set; } = new List<FavoritesItem>();
}