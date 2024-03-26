using eShop.Domain.Options;
using eShop.EmailSenderWebApi.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailOptions"));

builder.Services.AddMassTransit(x =>
{
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

        cfg.ReceiveEndpoint("reset-password", e =>
        {
            e.ConfigureConsumer<ResetPasswordEmailReceiver>(context);
        });

        cfg.ReceiveEndpoint("confirm-email", e =>
        {
            e.ConfigureConsumer<ConfirmEmailReceiver>(context);
        });
    });

    x.AddConsumer<ResetPasswordEmailReceiver>();
    x.AddConsumer<ConfirmEmailReceiver>();
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.Run();

