using eShop.Domain.Messages;
using eShop.Domain.Options;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eShop.EmailSenderApi.Services
{
    public class AccountRegisteredReceiver(IOptions<EmailOptions> _options) : IConsumer<AccountRegisteredMessage>
    {
        private readonly EmailOptions options = _options.Value;
        public async Task Consume(ConsumeContext<AccountRegisteredMessage> context)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(options.DisplayName, options.Email));
            emailMessage.To.Add(new MailboxAddress(context.Message.To, context.Message.To));
            emailMessage.Subject = context.Message.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = GetEmailBody(context.Message.To);

            emailMessage.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(options.Host, options.Port, false);
                await client.AuthenticateAsync(options.Email, options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        private string GetEmailBody(string userName)
        {
            string body = @"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Welcome to eShop!</title>
            </head>
            <body>
                <p>Hello " + userName + @",</p>
                <p>Your account was successfully registered.</p>
                <p>Thank you,</p>
                <p>Your Website Team</p>
            </body>
            </html>";

            return body;
        }
    }
}
