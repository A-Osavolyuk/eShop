using eShop.Application;
using eShop.AuthWebApi.Services.Implementation;
using Microsoft.AspNetCore.Authentication.Google;

namespace eShop.AuthWebApi
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddServiceDefaults();
            builder.ConfigureVersioning();
            builder.AddMapping();
            builder.AddValidation();

            builder.Services.AddCors(o =>
            {
                o.AddDefaultPolicy(p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.AllowCredentials();
                    p.WithOrigins("https://localhost:8101");
                });
            });

            builder.AddAuth();
            builder.AddDependencyInjection();

            builder.AddSqlServerDbContext<AuthDbContext>("SqlServer");

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static IHostApplicationBuilder AddAuth(this IHostApplicationBuilder builder)
        {
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AuthDbContext>();

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

            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITokenHandler, TokenHandler>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            return builder;
        }
    }
}
