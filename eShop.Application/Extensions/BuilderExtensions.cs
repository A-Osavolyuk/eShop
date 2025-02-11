using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace eShop.Application.Extensions;

public static class BuilderExtensions
{
    public static IHostApplicationBuilder AddValidation(this IHostApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(BuilderExtensions));
        return builder;
    }

    public static IHostApplicationBuilder AddVersioning(this IHostApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = ApiVersion.Default;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("api-version"));
        });

        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = ApiVersion.Default;
            options.SubstituteApiVersionInUrl = true;
            options.GroupNameFormat = "'v'V";
        });

        return builder;
    }

    public static IHostApplicationBuilder AddJwtAuthentication(this IHostApplicationBuilder builder)
    {
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
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Configuration:Security:Authentication:JWT:Key"]!))
            };
        });

        return builder;
    }

    public static IHostApplicationBuilder AddRedisCache(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration["Configuration:Services:Cache:Redis:ConnectionString"]!;
        var instanceName = builder.Configuration["Configuration:Services:Cache:Redis:InstanceName"]!;
        
        builder.Services.AddSingleton<IConnectionMultiplexer>(sp => 
                ConnectionMultiplexer.Connect(connectionString));
        
        builder.Services.AddStackExchangeRedisCache(cfg =>
        {
            cfg.Configuration = connectionString;
            cfg.InstanceName = instanceName;
        });
        
        return builder;
    }
}