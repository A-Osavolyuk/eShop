using Microsoft.Extensions.Hosting;

namespace eShop.Infrastructure
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddInfrastructureLayer(this IHostApplicationBuilder builder)
        {
            return builder;
        }

    }
}
