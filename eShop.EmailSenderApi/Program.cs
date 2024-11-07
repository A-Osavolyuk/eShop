using eShop.Domain.Options;
using eShop.EmailSenderApi.Services;
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

        cfg.ReceiveEndpoint("reset-password", e => e.ConfigureConsumer<ResetPasswordEmailReceiver>(context));
        cfg.ReceiveEndpoint("change-email", e => e.ConfigureConsumer<ChangeEmailReceiver>(context));
        cfg.ReceiveEndpoint("change-phone-number", e => e.ConfigureConsumer<ChangePhoneNumberReceiver>(context));
        cfg.ReceiveEndpoint("confirm-email", e => e.ConfigureConsumer<ConfirmEmailReceiver>(context));
        cfg.ReceiveEndpoint("account-registered", e => e.ConfigureConsumer<AccountRegisteredReceiver>(context));
        cfg.ReceiveEndpoint("2fa-code", e => e.ConfigureConsumer<TwoFactorAuthenticationCodeReceiver>(context));
        cfg.ReceiveEndpoint("registered-on-external-login", e => e.ConfigureConsumer<AccountRegisteredOnExternalLoginReceiver>(context));
    });

    x.AddConsumer<ResetPasswordEmailReceiver>();
    x.AddConsumer<ChangeEmailReceiver>();
    x.AddConsumer<ChangePhoneNumberReceiver>();
    x.AddConsumer<ConfirmEmailReceiver>();
    x.AddConsumer<AccountRegisteredReceiver>();
    x.AddConsumer<TwoFactorAuthenticationCodeReceiver>();
    x.AddConsumer<AccountRegisteredOnExternalLoginReceiver>();
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.Run();

