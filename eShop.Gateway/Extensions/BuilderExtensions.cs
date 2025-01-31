using eShop.Application.Extensions;
using eShop.ServiceDefaults;

namespace eShop.Gateway.Extensions;

public static class BuilderExtensions
{
    public static void AppApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddServiceDefaults();
        builder.Logging.AddConfiguration(builder.Configuration.GetSection("Configuration:Logging"));
        builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
        builder.AddJwtAuthentication();
    }
}