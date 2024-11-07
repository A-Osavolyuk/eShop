using eShop.Application.Behaviours;
using eShop.Application.Middlewares;
using eShop.ReviewsApi.Data;
using eShop.ReviewsApi.Receivers;
using FluentValidation;
using MassTransit;

namespace eShop.ReviewsApi.Extensions
{
    public static class BuilderExtensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddJwtAuthentication();
            builder.AddVersioning();
            builder.AddMapping();
            builder.AddSwaggerWithSecurity();
            builder.AddDependencyInjection();
            builder.AddMessageBus();

            builder.AddSqlServerDbContext<ReviewDbContext>("SqlServer");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMediatR(x =>
            {
                x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            });
            
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            return builder;
        }

        private static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
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

                    cfg.ReceiveEndpoint("product-deleted", e => e.ConfigureConsumer<ProductDeletedReceiver>(context));
                });

                x.AddConsumer<ProductDeletedReceiver>();
            });

            return builder;
        }
    }
}
