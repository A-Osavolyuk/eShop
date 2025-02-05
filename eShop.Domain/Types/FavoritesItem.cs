﻿namespace eShop.Domain.Types;

public class FavoritesItem
{
    [BsonRepresentation(BsonType.String)]
    public Guid ProductId { get; set; }
    public string ProductArticle { get; set; } = String.Empty;
    public DateTime AddedAt { get; set; } = DateTime.Now;
}