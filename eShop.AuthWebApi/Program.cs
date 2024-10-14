using eShop.Domain.Entities.Admin;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ConfigureDatabaseAsync<AuthDbContext>();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
