using Blazored.LocalStorage;
using eShop.Domain.Interfaces;
using eShop.Infrastructure.Account;
using eShop.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
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

            builder.Services.AddScoped<IHttpClientService, HttpClientService>();
            builder.Services.AddScoped<ITokenProvider, TokenProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ISuppliersService, SuppliersService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<ISubcategoriesService, SubcategoriesService>();

            return builder;
        }

    }
}
