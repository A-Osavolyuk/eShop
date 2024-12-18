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
        builder.AddSqlServerDbContext<AuthDbContext>("SqlServer");
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
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
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
                options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "";
                options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "";
            })
            .AddFacebook(options =>
            {
                options.ClientId = builder.Configuration["Authentication:Facebook:ClientId"] ?? "";
                options.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"] ?? "";
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
                    ValidAudience = builder.Configuration["JwtOptions:Audience"],
                    ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]!))
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
                var uri = builder.Configuration["RabbitMQConfigurations:HostUri"]!;
                var username = builder.Configuration["RabbitMQConfigurations:UserName"]!;
                var password = builder.Configuration["RabbitMQConfigurations:Password"]!;

                cfg.Host(new Uri(uri), h =>
                {
                    h.Username(username);
                    h.Password(password);
                });
            });
        });
    }
}