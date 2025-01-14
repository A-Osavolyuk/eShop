using eShop.Domain.Messages;
using eShop.Domain.Messages.Email;
using eShop.Domain.Options;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eShop.EmailSender.Api.Receivers;

public class ChangeEmailConsumer(IOptions<EmailOptions> options) : IConsumer<ChangeEmailMessage>
{
    private readonly EmailOptions options = options.Value;

    public async Task Consume(ConsumeContext<ChangeEmailMessage> context)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(options.DisplayName, options.Email));
        emailMessage.To.Add(new MailboxAddress(context.Message.To, context.Message.To));
        emailMessage.Subject = context.Message.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = GetEmailBody(context.Message.To, context.Message.Code, context.Message.NewEmail);

        emailMessage.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(options.Host, options.Port, false);
            await client.AuthenticateAsync(options.Email, options.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }

    private string GetEmailBody(string userName, string code, string newEmail)
    {
        string body = $"""
                        <!DOCTYPE html>
                      <html lang="en">
                      <head>
                          <meta charset="UTF-8">
                          <meta name="viewport" content="width=device-width, initial-scale=1.0">
                          <title>Email change (step one)</title>
                      </head>
                      <body>
                      <div style="border: 1px solid rgb(190, 189, 189); width: 800px; margin: auto; padding: 1px;">
                          <div style="display: flex; justify-content: center; justify-items: center;">
                              <p style="font: bold 24px Arial, sans-serif; color: rgb(141, 66, 212);">eShop Team</p>
                          </div>
                          <div style="border: 1px solid rgb(190, 189, 189); width: 100%;"></div>
                          <div style="padding: 50px 100px; margin: auto;">
                              <h1 style="font: bold 24px Arial, sans-serif; margin: 0; margin-bottom: 40px;">Email change (step one)</h1>
                              <p style="font: 16px Arial, sans-serif; margin:0;">Hello, {userName}!.</p>
                              <br>
                              <p style="font: 16px Arial, sans-serif; margin: 0;">
                                  We received a request to change your current email to a new email: {newEmail}.
                                  To confirm the first step of an email change, please enter the confirmation code from this letter.
                              </p>
                              <br>
                              <p style="font: 16px Arial, sans-serif; margin: 0;"> Your confirmation code: {code}. Will expire in: 10 mins.</p>
                              <br>
                              <p style="font: 16px Arial, sans-serif; margin: 0;">
                                  Please do not give this code to anyone under any circumstances.
                              </p>
                              <br>
                              <p style="font: 16px Arial, sans-serif; margin: 0;">
                                  If you didn't request an email change, please contact our <a href="#">technical support</a>,
                                  change your password to a more secure one and turn on 2FA (two-factor authentication).
                                  We will help you to configure you account security. You can know more about an account security <a href="#">here</a>.
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