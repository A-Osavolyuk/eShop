using eShop.Domain.Options;
using eShop.EmailSenderApi.Receivers;
using eShop.ServiceDefaults;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("Configuration:Services:SMTP"));

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var uri = builder.Configuration["Configuration:Services:MessageBus:RabbitMq:HostUri"]!;
        var username = builder.Configuration["Configuration:Services:MessageBus:RabbitMq:UserName"]!;
        var password = builder.Configuration["Configuration:Services:MessageBus:RabbitMq:Password"]!;

        cfg.Host(new Uri(uri), h =>
        {
            h.Username(username);
            h.Password(password);
        });

        cfg.ReceiveEndpoint("reset-password", e => e.ConfigureConsumer<ResetPasswordEmailConsumer>(context));
        cfg.ReceiveEndpoint("change-email", e => e.ConfigureConsumer<ChangeEmailConsumer>(context));
        cfg.ReceiveEndpoint("change-phone-number", e => e.ConfigureConsumer<ChangePhoneNumberConsumer>(context));
        cfg.ReceiveEndpoint("confirm-email", e => e.ConfigureConsumer<ConfirmEmailConsumer>(context));
        cfg.ReceiveEndpoint("account-registered", e => e.ConfigureConsumer<AccountRegisteredConsumer>(context));
        cfg.ReceiveEndpoint("2fa-code", e => e.ConfigureConsumer<TwoFactorAuthenticationCodeConsumer>(context));
        cfg.ReceiveEndpoint("registered-on-external-login", e => e.ConfigureConsumer<ExternalLoginConsumer>(context));
    });

    x.AddConsumer<ResetPasswordEmailConsumer>();
    x.AddConsumer<ChangeEmailConsumer>();
    x.AddConsumer<ChangePhoneNumberConsumer>();
    x.AddConsumer<ConfirmEmailConsumer>();
    x.AddConsumer<AccountRegisteredConsumer>();
    x.AddConsumer<TwoFactorAuthenticationCodeConsumer>();
    x.AddConsumer<ExternalLoginConsumer>();
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.Run();

