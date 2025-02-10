using eShop.ServiceDefaults;

namespace eShop.FilesStorage.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void MapAppServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.MapDefaultEndpoints();
        
        app.Run();
    }
}