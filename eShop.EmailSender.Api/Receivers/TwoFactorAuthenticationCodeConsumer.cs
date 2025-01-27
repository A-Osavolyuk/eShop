using eShop.Domain.Messages;
using eShop.Domain.Messages.Email;
using eShop.EmailSender.Api.Options;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eShop.EmailSender.Api.Receivers;

public class TwoFactorAuthenticationCodeConsumer(IOptions<EmailOptions> options)
    : IConsumer<TwoFactorAuthenticationCodeMessage>
{
    private readonly EmailOptions options = options.Value;

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
        string body = $"""
                       <!DOCTYPE html>
                       <html lang="en">
                       <head>
                           <meta charset="UTF-8">
                           <meta name="viewport" content="width=device-width, initial-scale=1.0">
                           <title>Two-factor authentication</title>
                       </head>
                       <body>
                       <div style="border: 1px solid rgb(190, 189, 189); width: 800px; margin: auto; padding: 1px;">
                           <div style="display: flex; justify-content: center; justify-items: center;">
                               <p style="font: bold 24px Arial, sans-serif; color: rgb(141, 66, 212);">eShop Team</p>
                           </div>
                           <div style="border: 1px solid rgb(190, 189, 189); width: 100%;"></div>
                           <div style="padding: 50px 100px; margin: auto;">
                               <h1 style="font: bold 24px Arial, sans-serif; margin: 0; margin-bottom: 40px;">Two-factor authentication</h1>
                               <p style="font: 16px Arial, sans-serif; margin:0;">Hello, {userName}!.</p>
                               <br>
                               <p style="font: 16px Arial, sans-serif; margin: 0;">
                                    We received a request to sign in with 2-fa.
                               </p>
                               <br>
                               <p style="font: 16px Arial, sans-serif; margin: 0;"> Your 2-fa code: {code}. Will expire in: 10 mins.</p>
                               <br>
                               <p style="font: 16px Arial, sans-serif; margin: 0;">
                                    Please do not give this code to anyone under any circumstances.
                               </p>
                               <br>
                               <p style="font: 16px Arial, sans-serif; margin: 0;">eShop Team.</p>
                               <div style="border: 1px solid rgb(190, 189, 189); width: 100%; margin-top: 40px;"></div>
                               <p style="font: 14px Arial, sans-serif; margin:40px 0;">In case you have any questions, please <a href="#">contact support here</a></p>
                           </div >
                       </div>
                       </body>
                       </html>
                       """;

        return body;
    }
}