var builder = WebApplication.CreateBuilder(args);

builder.AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.MapApiServices();

app.Run();