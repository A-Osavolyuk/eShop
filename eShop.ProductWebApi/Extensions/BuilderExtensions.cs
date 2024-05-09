using eShop.Domain.DTOs.Requests;
using eShop.ProductWebApi.Behaviors;
using eShop.ProductWebApi.Commands.Products;
using eShop.ProductWebApi.Repositories;
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

            builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

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
            builder.Services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            builder.Services.AddScoped<IBrandsRepository, BrandsRepository>();

            //Create product commands
            builder.Services.AddTransient<IRequestHandler<CreateProductCommand<CreateClothingRequest, ClothingDTO>, Result<IEnumerable<ClothingDTO>>>, 
                CreateProductCommandHandler<CreateClothingRequest, ClothingDTO, Clothing>>();
            builder.Services.AddTransient<IRequestHandler<CreateProductCommand<CreateShoesRequest, ShoesDTO>, Result<IEnumerable<ShoesDTO>>>,
                CreateProductCommandHandler<CreateShoesRequest, ShoesDTO, Shoes>>();

            //Update product commands
            builder.Services.AddTransient<IRequestHandler<UpdateProductCommand<UpdateClothingRequest, ClothingDTO>, Result<ClothingDTO>>,
                UpdateProductCommandHandler<UpdateClothingRequest, ClothingDTO, Clothing>>();
            builder.Services.AddTransient<IRequestHandler<UpdateProductCommand<UpdateShoesRequest, ShoesDTO>, Result<ShoesDTO>>,
                UpdateProductCommandHandler<UpdateShoesRequest, ShoesDTO, Shoes>>();

            return builder;
        }
    }
}
