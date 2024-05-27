using eShop.Domain.DTOs.Requests;
using eShop.ProductWebApi.Behaviors;
using eShop.ProductWebApi.Commands.Products;
using eShop.ProductWebApi.Repositories;
using eShop.ProductWebApi.Services;
using MediatR;

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

            builder.AddSqlServerDbContext<ProductDbContext>("SqlServer");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
                c.AddOpenBehavior(typeof(LoggingBehavior<,>));
                
            });

            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IProductRepository, ProductsRepository>();
            builder.Services.AddScoped<IBrandsRepository, BrandsRepository>();
            builder.Services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

            //Create product commands
            builder.Services.AddTransient<IRequestHandler<CreateProductCommand<ClothingDTO, Clothing>, Result<IEnumerable<ClothingDTO>>>, 
                CreateProductCommandHandler<ClothingDTO, Clothing>>();
            builder.Services.AddTransient<IRequestHandler<CreateProductCommand<ShoesDTO, Shoes>, Result<IEnumerable<ShoesDTO>>>,
                CreateProductCommandHandler<ShoesDTO, Shoes>>();

            //Update product commands
            builder.Services.AddTransient<IRequestHandler<UpdateProductCommand<UpdateClothingRequest, ClothingDTO>, Result<ClothingDTO>>,
                UpdateProductCommandHandler<UpdateClothingRequest, ClothingDTO, Clothing>>();
            builder.Services.AddTransient<IRequestHandler<UpdateProductCommand<UpdateShoesRequest, ShoesDTO>, Result<ShoesDTO>>,
                UpdateProductCommandHandler<UpdateShoesRequest, ShoesDTO, Shoes>>();

            return builder;
        }
    }
}
