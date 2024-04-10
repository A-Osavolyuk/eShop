using eShop.Domain.Entities;
using eShop.Domain.Enums;
using eShop.ProductWebApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();