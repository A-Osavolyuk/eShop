using eShop.Application.Extensions;
using eShop.ProductApi.Data;

namespace eShop.ProductApi.Extensions
{
    public static class BuilderExtensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddJwtAuthentication();
            builder.AddDependencyInjection();
            builder.AddVersioning();
            builder.AddSwaggerWithSecurity();
            builder.AddMessageBus();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.AddMongoDBClient("MongoDB");
            builder.AddSqlServerDbContext<AppDbContext>("SqlServer");

            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
                c.AddOpenBehavior(typeof(LoggingBehaviour<,>));
                c.AddOpenBehavior(typeof(ValidationBehavior<,>));
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
                x.AddRequestClient<DeleteCommentsRequest>();
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
