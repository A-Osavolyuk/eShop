﻿using eShop.Application;
using eShop.Application.Behaviours;
using eShop.Application.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace eShop.CartApi.Extensions
{
    public static class BuilderExtensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddJwtAuthentication();
            builder.AddVersioning();
            builder.AddSwaggerWithSecurity();
            builder.AddDependencyInjection();
            builder.AddMessageBus();

            builder.AddMongoDBClient("MongoDB");

            builder.Services.AddControllers();
            
            builder.Services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
                x.AddOpenBehavior(typeof(LoggingBehaviour<,>), ServiceLifetime.Transient);
                x.AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Transient);
            });
            
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            return builder;
        }

        private static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            return builder;
        }

        private static IHostApplicationBuilder AddMessageBus(this IHostApplicationBuilder builder)
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

            return builder;
        }
    }
}