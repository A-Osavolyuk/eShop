using Amazon;
using Amazon.SimpleNotificationService;

namespace eShop.SmsSenderApi.Extensions;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddJwtAuthentication();
        builder.AddVersioning();
        builder.AddValidation();
        builder.AddSwaggerWithSecurity();
        builder.AddDependencyInjection();
        //builder.AddMessageBus();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            x.AddOpenBehavior(typeof(LoggingBehaviour<,>), ServiceLifetime.Transient);
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        builder.Services.AddSingleton<IAmazonSimpleNotificationService>(sp => new AmazonSimpleNotificationServiceClient(RegionEndpoint.EUNorth1));

        return builder;
    }

    private static void AddDependencyInjection(this IHostApplicationBuilder builder)
    {
        
    }

    private static void AddMessageBus(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var uri = builder.Configuration["Configuration:Services:MessageBus:RabbitMq:HostUri"]!;
                var username = builder.Configuration["Configuration:Services:MessageBus:RabbitMq:UserName"]!;
                var password = builder.Configuration["Configuration:Services:MessageBus:RabbitMq:Password"]!;

                cfg.Host(new Uri(uri), h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
            
        });
    }
}