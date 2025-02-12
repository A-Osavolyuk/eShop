﻿using eShop.Application.Documentation.Transformers;
using eShop.SmsSender.Api.Interfaces;

namespace eShop.SmsSender.Api.Extensions;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Configuration:Logging"));

        builder.AddServiceDefaults();
        builder.AddJwtAuthentication();
        builder.AddVersioning();
        builder.AddValidation();
        builder.AddDependencyInjection();
        builder.AddMessageBus();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            x.AddOpenBehavior(typeof(LoggingBehaviour<,>), ServiceLifetime.Transient);
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerTokenTransformer>();
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info.Title = "SMS sender API";
                document.Info.Version = "1.0.0";
                document.Info.Description = "This API contains methods for sending SMS messages";

                return Task.CompletedTask;
            });
        });

        return builder;
    }

    private static void AddDependencyInjection(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISmsService, SmsService>();
        builder.Services.AddSingleton<IAmazonSimpleNotificationService>(sp =>
            new AmazonSimpleNotificationServiceClient(RegionEndpoint.EUNorth1));
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

                cfg.ReceiveEndpoint("phone-number-verification",
                    e => e.ConfigureConsumer<VerifyPhoneNumberConsumer>(context));
                cfg.ReceiveEndpoint("change-phone-number",
                    e => e.ConfigureConsumer<ChangePhoneNumberConsumer>(context));
            });

            x.AddConsumer<VerifyPhoneNumberConsumer>();
            x.AddConsumer<ChangePhoneNumberConsumer>();
        });
    }
}