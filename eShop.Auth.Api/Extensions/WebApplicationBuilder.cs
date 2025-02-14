﻿namespace eShop.Auth.Api.Extensions;

public static class WebApplicationBuilder
{
    public static async Task MapApiServices(this WebApplication app)
    {
        app.MapDefaultEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
            await app.ConfigureDatabaseAsync<AuthDbContext>();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthorization();
        app.MapControllers();
        app.MapGrpcService<AuthServer>();
    }
}