﻿using System.Net.Mime;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using eShop.Domain.Enums;

namespace eShop.Domain.Entities.Product;

public class ProductEntity
{
    public ProductEntity() => Article = GenerateArticle();

    [BsonId, BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public ProductTypes ProductType { get; set; } = ProductTypes.None;
    public string Article { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Price Price { get; set; } =  new Price { Amount = 0, Currency = Currency.None };
    public List<string> Images { get; set; } = new List<string>();
    public BrandEntity Brand { get; set; } = new BrandEntity();

    private static string GenerateArticle() => new Random().NextInt64(100_000_000, 999_999_999_999).ToString();
}