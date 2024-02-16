using eShop.Application.Validation;
using eShop.Domain.DTOs.Requests;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eShop.Application;

public static class Extensions
{
	public static IHostApplicationBuilder AddApplicationLayer(this IHostApplicationBuilder builder)
	{
		builder.Services.AddValidatorsFromAssemblyContaining(typeof(Extensions));

		return builder;
	}

	public static IHostApplicationBuilder AddUserValidation(this IHostApplicationBuilder builder)
	{
		builder.Services.AddScoped<IValidator<RegistrationRequestDto>, RegistrationValidator>();
		builder.Services.AddScoped<IValidator<LoginRequestDto>, LoginValidator>();

		return builder;
	}
}