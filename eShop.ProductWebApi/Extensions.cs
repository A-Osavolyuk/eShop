namespace eShop.ProductWebApi
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddServiceDefaults();
            builder.AddJwtAuthentication();
            builder.AddDependencyInjection();
            builder.ConfigureVersioning();
            builder.AddMapping();
            builder.AddValidation();

            builder.Services.AddDbContext<ProductDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<IAssemblyMarker>());

            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ISubcategoriesRepository, SubcategoriesRepository>();
            builder.Services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

            return builder;
        }
    }
}
