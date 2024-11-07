using eShop.Application;

namespace eShop.FilesStorageWebApi.Extensions;

public static class BuilderExtensions
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddJwtAuthentication();
        builder.AddVersioning();
        builder.AddSwaggerWithSecurity();
        builder.AddServiceDefaults();
        builder.Services.AddControllers();
    }
}