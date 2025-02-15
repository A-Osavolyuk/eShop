﻿namespace eShop.EmailSender.Api.Extensions;

public static class WebApplicationBuilder
{
    public static void MapApiServices(this WebApplication app)
    {
        app.MapDefaultEndpoints();
        app.UseHttpsRedirection();
        app.MapOpenApi();
        app.MapScalarApiReference();
    }
}