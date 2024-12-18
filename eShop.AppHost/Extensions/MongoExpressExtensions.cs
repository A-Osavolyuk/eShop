using Aspire.Hosting.MongoDB;

namespace eShop.AppHost.Extensions;

public static class MongoExpressExtensions
{
    public static IResourceBuilder<MongoExpressContainerResource> WithAuthentication(
        this IResourceBuilder<MongoExpressContainerResource> builder, string name = "admin", string password = "admin")
    {
        builder.WithEnvironment("ME_CONFIG_BASICAUTH", "true");
        builder.WithEnvironment("ME_CONFIG_BASICAUTH_USERNAME", name);
        builder.WithEnvironment("ME_CONFIG_BASICAUTH_PASSWORD", password);
        
        return builder;
    }
    
    public static IResourceBuilder<MongoExpressContainerResource> WithMongoCredentials(
        this IResourceBuilder<MongoExpressContainerResource> builder, string name, string password)
    {
        builder.WithEnvironment("ME_CONFIG_MONGODB_ADMINUSERNAME", name); 
        builder.WithEnvironment("ME_CONFIG_MONGODB_ADMINPASSWORD", password);
        
        return builder;
    }
    
    public static IResourceBuilder<MongoExpressContainerResource> WithMongoServer(
        this IResourceBuilder<MongoExpressContainerResource> builder, string server)
    {
        builder.WithEnvironment("ME_CONFIG_MONGODB_SERVER", server);
        
        return builder;
    }
    
    public static IResourceBuilder<MongoExpressContainerResource> WithMongoUrl(
        this IResourceBuilder<MongoExpressContainerResource> builder, string url)
    {
        builder.WithEnvironment("ME_CONFIG_MONGODB_URL", url);
        
        return builder;
    }
}