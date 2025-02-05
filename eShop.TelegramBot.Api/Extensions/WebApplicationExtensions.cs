namespace eShop.TelegramBot.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void MapApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapDefaultEndpoints();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseExceptionHandler();
    }
}