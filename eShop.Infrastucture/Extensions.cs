using Blazored.LocalStorage;
using eShop.Domain.Interfaces;
using eShop.Infrastructure.Account;
using eShop.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Azure;
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

        private static IHostApplicationBuilder AddDependencyInjection(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<AuthenticationStateProvider, ApplicationAuthenticationStateProvider>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddHttpClient<IProductService, ProductService>();
            builder.Services.AddHttpClient<IBrandService, BrandSevice>();

            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddScoped<ITokenProvider, TokenProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBrandService, BrandSevice>();
            builder.Services.AddScoped<ILocalDataAccessor, LocalDataAccessor>();
            builder.Services.AddScoped<IStoreService, StoreService>();

            return builder;
        }

    }
}
