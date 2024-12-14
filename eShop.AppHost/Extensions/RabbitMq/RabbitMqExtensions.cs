namespace eShop.AppHost.Extensions.RabbitMq;

public static class RabbitMqExtensions
{
    public static IResourceBuilder<RabbitMQServerResource> WithAuthentication(
        this IResourceBuilder<RabbitMQServerResource> builder, string username, string password)
    {
        builder.WithEnvironment("RABBITMQ_DEFAULT_USER", username);
        builder.WithEnvironment("RABBITMQ_DEFAULT_PASS", password);
        
        return builder;
    }
}