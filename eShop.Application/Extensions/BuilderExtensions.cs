using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
    
    public static IHostApplicationBuilder AddSwaggerWithSecurity(this IHostApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
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
                    }, []
                }
            });
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
        builder.Services.AddStackExchangeRedisCache(cfg =>
        {
            cfg.Configuration = builder.Configuration["Configuration:Services:Cache:Redis:ConnectionString"];
            cfg.InstanceName = builder.Configuration["Configuration:Services:Cache:Redis:InstanceName"];
        });
        
        return builder;
    }
}