using eShop.Application;
using eShop.Application.Extensions;
using eShop.ServiceDefaults;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Configuration:Logging"));
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
