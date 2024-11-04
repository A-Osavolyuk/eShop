using eShop.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eShop.Domain.Entities.Product;

public class ShoesEntity : ProductEntity
{
    public ProductColor Color { get; set; } = ProductColor.None;
    public HashSet<ProductSize> Size { get; set; } = new HashSet<ProductSize>();
    public Audience Audience { get; set; } = Audience.None;
}