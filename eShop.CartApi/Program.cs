var builder = WebApplication.CreateBuilder(args);

builder.AddApiServices();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //await app.SeedDataAsync();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler();
app.MapGrpcService<CartServer>();

app.Run();
