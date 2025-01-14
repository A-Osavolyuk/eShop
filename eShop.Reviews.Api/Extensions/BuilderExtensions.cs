using eShop.Domain.Interfaces;
using eShop.Reviews.Api.Behaviours;
using eShop.Reviews.Api.Data;
using eShop.Reviews.Api.Receivers;
using eShop.Reviews.Api.Services.Implementation;

namespace eShop.Reviews.Api.Extensions;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Configuration:Logging"));
        
        builder.AddJwtAuthentication();
        builder.AddVersioning();
        builder.AddValidation();
        builder.AddSwaggerWithSecurity();
        builder.AddDependencyInjection();
        builder.AddMessageBus();
        builder.AddRedisCache();
        builder.Services.AddDbContext<AppDbContext>(cfg =>
        {
            cfg.UseSqlServer(builder.Configuration["Configuration:Storage:Databases:SQL:MSSQL:ConnectionString"]!);
        });
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            x.AddOpenBehavior(typeof(LoggingBehaviour<,>), ServiceLifetime.Transient);
            x.AddOpenBehavior(typeof(TransactionBehaviour<,>), ServiceLifetime.Transient);
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        return builder;
    }

    private static void AddDependencyInjection(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICacheService, CacheService>();
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

                cfg.ReceiveEndpoint("product-deleted", e => e.ConfigureConsumer<ProductDeletedReceiver>(context));
            });

            x.AddConsumer<ProductDeletedReceiver>();
        });
    }
}