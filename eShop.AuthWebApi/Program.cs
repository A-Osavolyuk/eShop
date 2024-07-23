using eShop.AuthWebApi.Receivers;
using eShop.Domain.DTOs.Requests.Cart;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddApiServices();

builder.Services.AddMassTransit(x =>
{
    x.AddRequestClient<CreateCartRequest>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var uri = builder.Configuration["RabbitMQConfigurations:HostUri"]!;
        var username = builder.Configuration["RabbitMQConfigurations:UserName"]!;
        var password = builder.Configuration["RabbitMQConfigurations:Password"]!;

        cfg.Host(new Uri(uri), h =>
        {
            h.Username(username);
            h.Password(password);
        });

        cfg.ReceiveEndpoint("user-exists", e => e.ConfigureConsumer<UserExistsReceiver>(context));
    });

    x.AddConsumer<UserExistsReceiver>();
});


var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
