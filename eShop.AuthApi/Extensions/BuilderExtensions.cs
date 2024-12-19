using eShop.Domain.Entities.AuthApi;
using eShop.Domain.Requests.CartApi.Cart;
using TokenHandler = eShop.AuthApi.Services.Implementation.TokenHandler;

namespace eShop.AuthApi.Extensions;

public static class BuilderExtensions
{
    public static void AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddVersioning();
        builder.AddValidation();
        builder.AddMessageBus();
        builder.AddSwaggerWithSecurity();
        builder.AddServiceDefaults();
        builder.AddAuth();
        builder.AddDependencyInjection();
        builder.Services.AddDbContext<AuthDbContext>(cfg =>
        {
            cfg.UseSqlServer(builder.Configuration["Configuration:Storage:Databases:SQL:MSSQL:ConnectionString"]!);
        });
        builder.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>();
            x.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });
        builder.Services.AddCors(o =>
        {
            o.AddDefaultPolicy(p =>
            {
                p.AllowAnyHeader();
                p.AllowAnyMethod();
                p.AllowAnyOrigin();
            });
        });
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Configuration:Security:Authentication:JWT"));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
    }

    private static void AddAuth(this IHostApplicationBuilder builder)
    {
        builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AuthDbContext>();

        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Configuration:Security:Authentication:Providers:Google:ClientId"] ?? "";
                options.ClientSecret = builder.Configuration["Configuration:Security:Authentication:Providers:Google:ClientSecret"] ?? "";
            })
            .AddFacebook(options =>
            {
                options.ClientId = builder.Configuration["Configuration:Security:Authentication:Providers:Facebook:ClientId"] ?? "";
                options.ClientSecret = builder.Configuration["Configuration:Security:Authentication:Providers:Facebook:ClientSecret"] ?? "";
            });

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["Configuration:Security:Authentication:JWT:Audience"],
                    ValidIssuer = builder.Configuration["Configuration:Security:Authentication:JWT:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Configuration:Security:Authentication:JWT:Key"]!))
                };
            });

        builder.Services.AddAuthorization(cfg =>
        {
            cfg.AddPolicy("ManageUsersPolicy",
                policy => policy.Requirements.Add(new PermissionRequirement("Permission.Admin.ManageUsers")));
            cfg.AddPolicy("ManageLockoutPolicy",
                policy => policy.Requirements.Add(new PermissionRequirement("Permission.Admin.ManageLockout")));
            cfg.AddPolicy("ManageRolesPolicy",
                policy => policy.Requirements.Add(new PermissionRequirement("Permission.Admin.ManageRoles")));
            cfg.AddPolicy("ManagePermissionsPolicy",
                policy => policy.Requirements.Add(new PermissionRequirement("Permission.Admin.ManagePermissions")));
            cfg.AddPolicy("ManageAccountPolicy",
                policy => policy.Requirements.Add(new PermissionRequirement("Permission.Account.ManageAccount")));
        });
    }

    private static void AddDependencyInjection(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ITokenHandler, TokenHandler>();
        builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder.Services.AddScoped<IPermissionManager, PermissionManager>();
        builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        builder.Services.AddHostedService<BackgroundTokenValidator>();

        builder.Services.AddScoped<AppManager>();
        builder.Services.AddScoped<CartClient>();
    }

    private static void AddMessageBus(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(x =>
        {
            x.AddRequestClient<CreateCartRequest>();
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