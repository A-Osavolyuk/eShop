namespace eShop.FilesStorageWebApi.Extensions;

public static class BuilderExtensions
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
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
            x.AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Transient);
        });
            
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
    }

    private static void AddDependencyInjection(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IStoreService, StoreService>();
    }
}