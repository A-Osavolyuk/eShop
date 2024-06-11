using eShop.ProductWebApi.Behaviors;
using eShop.ProductWebApi.Repositories;
using eShop.ProductWebApi.Services;
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
            builder.Services.AddScoped<IProductRepository, ProductsRepository>();
            builder.Services.AddScoped<IBrandsRepository, BrandsRepository>();
            builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

            return builder;
        }
    }
}
