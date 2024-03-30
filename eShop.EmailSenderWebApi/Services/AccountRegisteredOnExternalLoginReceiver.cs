using eShop.Domain.DTOs.Messages;
using eShop.Domain.Options;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eShop.EmailSenderWebApi.Services
{
    public class AccountRegisteredOnExternalLoginReceiver(IOptions<EmailOptions> _options) : IConsumer<AccountRegisteredOnExternalLoginMessage>
    {
        private readonly EmailOptions options = _options.Value;
        public async Task Consume(ConsumeContext<AccountRegisteredOnExternalLoginMessage> context)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(options.DisplayName, options.Email));
            emailMessage.To.Add(new MailboxAddress(context.Message.To, context.Message.To));
            emailMessage.Subject = context.Message.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = GetEmailBody(context.Message.To, context.Message.ProviderName, context.Message.TempPassword);

            emailMessage.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(options.Host, options.Port, false);
                await client.AuthenticateAsync(options.Email, options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        private string GetEmailBody(string userName, string providerName, string tempPassword)
        {
            string body = @$"
            <!DOCTYPE html>
            <html>
            <head>
                <title> Account registered with {providerName} sign in!</title>
            </head>
            <body>
                <p>Hello {userName} ,</p>
                <p>Your account was successfully registered with {providerName} sign in.</p>
                <p>Your temp password for password sign in: {tempPassword}.</p>
                <p>Thank you,</p>
                <p>Your Website Team</p>
            </body>
            </html>";

            return body;
        }
    }
}
