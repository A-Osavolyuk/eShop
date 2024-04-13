using eShop.Domain.Messages;
using eShop.Domain.Options;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.Extensions.Options;
using MimeKit;

namespace eShop.EmailSenderWebApi.Services
{
    public class ChangePhoneNumberReceiver(IOptions<EmailOptions> _options) : IConsumer<ChangePhoneNumberMessage>
    {
        private readonly EmailOptions options = _options.Value;

        public async Task Consume(ConsumeContext<ChangePhoneNumberMessage> context)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(options.DisplayName, options.Email));
            emailMessage.To.Add(new MailboxAddress(context.Message.To, context.Message.To));
            emailMessage.Subject = context.Message.Subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = GetEmailBody(context.Message.To, context.Message.Link, context.Message.PhoneNumber);

            emailMessage.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(options.Host, options.Port, false);
                await client.AuthenticateAsync(options.Email, options.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        private string GetEmailBody(string userName, string resetLink, string newPhoneNumber)
        {
            string body = @$"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Change Phone Number Request</title>
            </head>
            <body>
                <p>Hello {userName},</p>
                <p>We received a request to change your phone number to {newPhoneNumber}. Please click the link below to change your phone number:</p>
                <p><a href='{resetLink}'>Change Phone Number</a></p>
                <p>If you didn't request a phone number change, you can ignore this email.</p>
                <p>Thank you,</p>
                <p>Your Website Team</p>
            </body>
            </html>";

            return body;
        }
    }
}
