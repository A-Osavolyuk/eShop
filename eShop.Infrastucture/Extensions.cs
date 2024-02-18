using Blazored.LocalStorage;
using eShop.Domain.Interfaces;
using eShop.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShop.Infrastructure
{
    public static class Extensions
    {
        public static IHostApplicationBuilder AddInfrastructureLayer(this IHostApplicationBuilder builder)
        {
            builder.AddDependencyInjection();

            builder.Services.AddBlazoredLocalStorage();

            return builder;
        }

        public static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddScoped<ITokenProvider, TokenProvider>();

            return builder;
        }

    }
}
