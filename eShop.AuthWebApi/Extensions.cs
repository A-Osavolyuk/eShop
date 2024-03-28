using eShop.Application;
using eShop.AuthWebApi.Services.Implementation;

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
