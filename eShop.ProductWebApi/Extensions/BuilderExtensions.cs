using eShop.Domain.DTOs.Requests.Review;
using eShop.ProductWebApi.Behaviors;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace eShop.ProductWebApi.Extensions
{
    public static class BuilderExtensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddJwtAuthentication();
            builder.AddDependencyInjection();
            builder.ConfigureVersioning();
            builder.AddMapping();
            builder.AddValidation();
            builder.AddSwaggerWithSecurity();
            builder.AddMessageBus();

            builder.AddSqlServerDbContext<ProductDbContext>("SqlServer");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
                c.AddOpenBehavior(typeof(LoggingBehavior<,>));

            });

            return builder;
        }

        public static IHostApplicationBuilder AddSwaggerWithSecurity(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: 'Bearer Generated-JWT-Token'",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        }, new string[] { }
                    }
                });
            });
            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {

            return builder;
        }

        public static IHostApplicationBuilder AddMessageBus(this IHostApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.AddRequestClient<DeleteReviewsRequest>();
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
