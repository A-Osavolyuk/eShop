var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRabbitMQ("RabbitMQ");

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.Run();

