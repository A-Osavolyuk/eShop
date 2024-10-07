using eShop.Application;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddOcelot();

builder.AddJwtAuthentication();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.UseAuthorization();
});

app.Run();
