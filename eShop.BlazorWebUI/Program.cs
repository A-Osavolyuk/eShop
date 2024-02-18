using eShop.Application;
using eShop.BlazorWebUI.Components;
using eShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCascadingAuthenticationState();

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.AddApplicationLayer();
builder.AddInfrastructureLayer();

var app = builder.Build();

app.MapDefaultEndpoints();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
