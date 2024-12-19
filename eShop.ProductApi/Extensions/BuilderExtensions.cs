namespace eShop.ProductApi.Extensions;

public static class BuilderExtensions
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.AddJwtAuthentication();
        builder.AddDependencyInjection();
        builder.AddVersioning();
        builder.AddSwaggerWithSecurity();
        builder.AddMessageBus();
        builder.AddValidation();
        builder.Services.AddDbContext<AppDbContext>(cfg =>
        {
            cfg.UseSqlServer(builder.Configuration["Configuration:Storage:Databases:SQL:MSSQL:ConnectionString"]!);
        });
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            c.AddOpenBehavior(typeof(LoggingBehaviour<,>), ServiceLifetime.Transient);
            c.AddOpenBehavior(typeof(TransactionBehaviour<,>), ServiceLifetime.Transient);
        });
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
    }

    private static void AddDependencyInjection(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<AuthClient>();
    }

    private static void AddMessageBus(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                var uri = builder.Configuration["Configuration:Services:MessageBust:RabbitMq:HostUri"]!;
                var username = builder.Configuration["Configuration:Services:MessageBust:RabbitMq:UserName"]!;
                var password = builder.Configuration["Configuration:Services:MessageBust:RabbitMq:Password"]!;

                cfg.Host(new Uri(uri), h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
        });
    }
}