using eShop.Domain.Messages;
using eShop.Domain.Options;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eShop.EmailSenderApi.Receivers
{
    public class ChangeEmailReceiver(IOptions<EmailOptions> _options) : IConsumer<ChangeEmailMessage>
    {
        private readonly EmailOptions options = _options.Value;

        public async Task Consume(ConsumeContext<ChangeEmailMessage> context)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(options.DisplayName, options.Email));
            emailMessage.To.Add(new MailboxAddress(context.Message.To, context.Message.To));
            emailMessage.Subject = context.Message.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = GetEmailBody(context.Message.To, context.Message.Link, context.Message.NewEmail);

            emailMessage.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(options.Host, options.Port, false);
                await client.AuthenticateAsync(options.Email, options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        private string GetEmailBody(string userName, string resetLink, string newEmail)
        {
            string body = @$"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Change Email Request</title>
            </head>
            <body>
                <p>Hello {userName},</p>
                <p>We received a request to change your email to {newEmail}. Please click the link below to change your email:</p>
                <p><a href='{resetLink}'>Change Email</a></p>
                <p>If you didn't request an email change, you can ignore this email.</p>
                <p>Thank you,</p>
                <p>Your Website Team</p>
            </body>
            </html>";

            return body;
        }
    }
}
