using eShop.Application.Extensions;
using eShop.Domain.Interfaces;
using eShop.FilesStorageApi.Services;
using eShop.FilesStorageApi.Services.Implementation;
using eShop.ServiceDefaults;
using IStoreService = eShop.FilesStorageApi.Services.Interfaces.IStoreService;

namespace eShop.FilesStorageApi.Extensions;

public static class BuilderExtensions
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Configuration:Logging"));
        
        builder.AddJwtAuthentication();
        builder.AddVersioning();
        builder.AddSwaggerWithSecurity();
        builder.AddDependencyInjection();
        builder.AddServiceDefaults();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            x.AddOpenBehavior(typeof(LoggingBehaviour<,>), ServiceLifetime.Transient);
        });
            
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
    }

    private static void AddDependencyInjection(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IStoreService, StoreService>();
        builder.Services.AddScoped<ICacheService, CacheService>();
    }
}