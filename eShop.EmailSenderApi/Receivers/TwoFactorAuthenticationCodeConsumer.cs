using eShop.Domain.Messages;
using eShop.Domain.Options;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eShop.EmailSenderApi.Receivers;

public class TwoFactorAuthenticationCodeConsumer(IOptions<EmailOptions> _options) : IConsumer<TwoFactorAuthenticationCodeMessage>
{
    private readonly EmailOptions options = _options.Value;

    public async Task Consume(ConsumeContext<TwoFactorAuthenticationCodeMessage> context)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(options.DisplayName, options.Email));
        emailMessage.To.Add(new MailboxAddress(context.Message.To, context.Message.To));
        emailMessage.Subject = context.Message.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = GetEmailBody(context.Message.To, context.Message.Code);

        emailMessage.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(options.Host, options.Port, false);
            await client.AuthenticateAsync(options.Email, options.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }

    private string GetEmailBody(string userName, string code)
    {
        string body = @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Log in with 2FA code</title>
            </head>
            <body>
                <p>Hello " + userName + @",</p>
                <p>We received a request to log in with 2FA code.</p>
                <p>Your 2FA code:" + code + @"</p>
                <p>If you didn't request 2FA code, you can ignore this email.</p>
                <p>Thank you,</p>
                <p>Your Website Team</p>
            </body>
            </html>";

        return body;
    }
}