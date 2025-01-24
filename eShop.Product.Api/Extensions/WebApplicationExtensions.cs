using eShop.Domain.Entities.Api.Product;

namespace eShop.Product.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureMongoDB(this WebApplication app)
    {
        RegisterClassMaps();
    }
    
    private static void RegisterClassMaps()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(ProductEntity)))
        {
            BsonClassMap.RegisterClassMap<ProductEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.SetDiscriminator("Product");
            });
            
            BsonClassMap.RegisterClassMap<ShoesEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("Shoes");
            });
            BsonClassMap.RegisterClassMap<ClothingEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetDiscriminator("Clothing");
            });
        }
    }
}