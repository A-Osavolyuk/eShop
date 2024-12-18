using eShop.CartApi.Data;

namespace eShop.CartApi.Extensions;

public static class BuilderExtensions
{
    [Obsolete("Obsolete")]
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddJwtAuthentication();
        builder.AddVersioning();
        builder.AddSwaggerWithSecurity();
        builder.AddDependencyInjection();
        builder.AddMessageBus();
        builder.AddValidation();
        builder.AddServiceDefaults();
        builder.Services.AddGrpc();
        builder.Services.AddControllers();
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            x.AddOpenBehavior(typeof(LoggingBehaviour<,>), ServiceLifetime.Transient);
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
    }

    private static void AddDependencyInjection(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
        builder.Services.AddSingleton<DbClient>();
    }

    private static void AddMessageBus(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var uri = builder.Configuration["RabbitMQConfigurations:HostUri"]!;
                var username = builder.Configuration["RabbitMQConfigurations:UserName"]!;
                var password = builder.Configuration["RabbitMQConfigurations:Password"]!;

                cfg.Host(new Uri(uri), h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
        });
    }
}