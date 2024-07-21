using eShop.Application;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.AddJwtAuthentication();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.UseOcelot();

app.Run();
