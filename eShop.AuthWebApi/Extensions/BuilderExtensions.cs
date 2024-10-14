using eShop.Application;
using eShop.AuthWebApi.BackgroundServices;
using eShop.AuthWebApi.Receivers;
using eShop.AuthWebApi.Security.Authorization;
using eShop.AuthWebApi.Services.Implementation;
using eShop.Domain.DTOs.Requests.Cart;
using eShop.Domain.Entities.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace eShop.AuthWebApi.Extensions
{
    public static class BuilderExtensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.ConfigureVersioning();
            builder.AddMapping();
            builder.AddValidation();
            builder.AddMessageBus();
            builder.AddSwaggerWithSecurity();
            builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<IAssemblyMarker>());
            builder.Services.AddCors(o =>
            {
                o.AddDefaultPolicy(p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowAnyOrigin();
                });
            });

            builder.AddAuth();
            builder.AddDependencyInjection();

            builder.AddSqlServerDbContext<AuthDbContext>("SqlServer");

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            return builder;
        }

        public static IHostApplicationBuilder AddAuth(this IHostApplicationBuilder builder)
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]!))
                };
            });

            builder.Services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("ManageUsersPolicy", policy => policy.Requirements.Add(new PermissionRequirement("Permission.Admin.ManageUsers")));
                cfg.AddPolicy("ManageLockoutPolicy", policy => policy.Requirements.Add(new PermissionRequirement("Permission.Admin.ManageLockout")));
                cfg.AddPolicy("ManageRolesPolicy", policy => policy.Requirements.Add(new PermissionRequirement("Permission.Admin.ManageRoles")));
                cfg.AddPolicy("ManagePermissionsPolicy", policy => policy.Requirements.Add(new PermissionRequirement("Permission.Admin.ManagePermissions")));
                cfg.AddPolicy("ManageAccountPolicy", policy => policy.Requirements.Add(new PermissionRequirement("Permission.Account.ManageAccount")));
            });

            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITokenHandler, TokenHandler>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IPermissionManager, PermissionManager>();
            builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            builder.Services.AddHostedService<BackgroundTokenValidator>();

            builder.Services.AddScoped<AppManager>();

            return builder;
        }

        public static IHostApplicationBuilder AddMessageBus(this IHostApplicationBuilder builder)
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

                    cfg.ReceiveEndpoint("user-exists", e => e.ConfigureConsumer<UserExistsReceiver>(context));
                });

                x.AddConsumer<UserExistsReceiver>();
            });

            return builder;
        }

        public static IHostApplicationBuilder AddSwaggerWithSecurity(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: 'Bearer Generated-JWT-Token'",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        }, new string[] { }
                    }
                });
            });
            return builder;
        }
    }
}
