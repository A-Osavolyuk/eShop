using eShop.Domain.Messages;
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

        cfg.ReceiveEndpoint("password-reset", e => e.ConfigureConsumer<ResetPasswordConsumer>(context));
        cfg.ReceiveEndpoint("email-change", e => e.ConfigureConsumer<ChangeEmailConsumer>(context));
        cfg.ReceiveEndpoint("email-verification", e => e.ConfigureConsumer<VerifyEmailConsumer>(context));
        cfg.ReceiveEndpoint("email-verified", e => e.ConfigureConsumer<EmailVerifiedConsumer>(context));
        cfg.ReceiveEndpoint("2fa-code", e => e.ConfigureConsumer<TwoFactorAuthenticationCodeConsumer>(context));
        cfg.ReceiveEndpoint("external-provider-registration", e => e.ConfigureConsumer<ExternalLoginConsumer>(context));
        cfg.ReceiveEndpoint("new-email-verification", e => e.ConfigureConsumer<VerifyNewEmailConsumer>(context));
    });

    x.AddConsumer<ResetPasswordConsumer>();
    x.AddConsumer<ChangeEmailConsumer>();
    x.AddConsumer<VerifyEmailConsumer>();
    x.AddConsumer<EmailVerifiedConsumer>();
    x.AddConsumer<TwoFactorAuthenticationCodeConsumer>();
    x.AddConsumer<ExternalLoginConsumer>();
    x.AddConsumer<VerifyNewEmailConsumer>();
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.Run();

