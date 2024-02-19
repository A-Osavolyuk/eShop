using eShop.Application;
using eShop.ProductWebApi.Data;

namespace eShop.ProductWebApi
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddServiceDefaults();
            builder.AddApiApplicationLayer();
            builder.AddJwtAuthentication();

            builder.AddDependencyInjection();

            builder.AddSqlServerDbContext<ProductDbContext>("SqlServer");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            return builder;
        }
    }
}
