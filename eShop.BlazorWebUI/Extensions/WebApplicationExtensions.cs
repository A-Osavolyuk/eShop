namespace eShop.BlazorWebUI.Extensions;

public static class WebApplicationExtensions
{
    public static void MapAppServices(this WebApplication app)
    {
        app.MapDefaultEndpoints();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseAntiforgery();
        app.UseStatusCodePagesWithRedirects("/Error?code={0}");

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
    }
}