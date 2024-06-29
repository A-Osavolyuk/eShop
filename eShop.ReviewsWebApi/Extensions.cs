using eShop.ReviewsWebApi.Receivers;
using eShop.ReviewsWebApi.Repositories;
using MassTransit;

namespace eShop.ReviewsWebApi
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddJwtAuthentication();
            builder.ConfigureVersioning();
            builder.AddMapping();
            builder.AddValidation();
            builder.AddSwaggerWithSecurity();
            builder.AddDependencyInjection();
            builder.AddMessageBus();

            builder.AddSqlServerDbContext<ReviewsDbContext>("SqlServer");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>());

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
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

            return builder;
        }

        public static IHostApplicationBuilder AddMessageBus(this IHostApplicationBuilder builder)
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
