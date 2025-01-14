﻿using eShop.CartApi.Services.Implementation;

namespace eShop.CartApi.Extensions;

public static class BuilderExtensions
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Configuration:Logging"));
        
        builder.AddJwtAuthentication();
        builder.AddVersioning();
        builder.AddSwaggerWithSecurity();
        builder.AddDependencyInjection();
        builder.AddMessageBus();
        builder.AddValidation();
        builder.AddServiceDefaults();
        builder.AddRedisCache();
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
        builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("Configuration:Storage:Databases:NoSQL:Mongo"));
        
        builder.Services.AddScoped<ICacheService, CacheService>();
        
        builder.Services.AddSingleton<DbClient>();
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