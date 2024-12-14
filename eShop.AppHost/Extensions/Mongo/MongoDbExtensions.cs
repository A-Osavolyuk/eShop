namespace eShop.AppHost.Extensions.Mongo;

public static class MongoDbExtensions
{
    public static IResourceBuilder<MongoDBServerResource> WithAuthentication(
        this IResourceBuilder<MongoDBServerResource> builder, string username, string password)
    {
        builder.WithEnvironment(EnvironmentVariables.MongoUserName, username);
        builder.WithEnvironment(EnvironmentVariables.MongoUserPassword, password);
        
        return builder;
    }
}