using eShop.ServiceDefaults;

namespace eShop.Gateway.Extensions;

public static class WebApplicationExtensions
{
    public static void MapApiServices(this WebApplication app)
    {
        app.MapDefaultEndpoints();
        app.MapReverseProxy(proxyPipeline =>
        {
            proxyPipeline.UseAuthorization();
        });
    }
}