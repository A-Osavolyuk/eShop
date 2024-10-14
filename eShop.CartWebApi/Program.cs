var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ConfigureDatabaseAsync<CartDbContext>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
