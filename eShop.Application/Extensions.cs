using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShop.Application;

public static class Extensions
{
    public static IHostApplicationBuilder AddApplicationLayer(this IHostApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(Extensions));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return builder;
    }
}