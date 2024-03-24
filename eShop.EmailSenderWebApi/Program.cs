using eShop.EmailSenderWebApi.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("user");
            h.Password("Test_12345");
        });

        cfg.ReceiveEndpoint("send-email", e =>
        {
            e.Consumer<EmailReceiver>();
        });
    });

    x.AddConsumer<EmailReceiver>();
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.Run();

