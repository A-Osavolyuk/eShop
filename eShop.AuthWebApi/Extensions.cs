using eShop.Application;
using eShop.AuthWebApi.Data;
using eShop.AuthWebApi.Services.Implementation;
using eShop.AuthWebApi.Services.Interfaces;
using eShop.Domain.Entities;
using eShop.Domain.Options;
using Microsoft.AspNetCore.Identity;

namespace eShop.AuthWebApi
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
        {
            builder.AddServiceDefaults();
            builder.AddApiApplicationLayer();

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
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AuthDbContext>();

            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITokenHandler, TokenHandler>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            return builder;
        }
    }
}
