using eShop.Domain.Messages;

namespace eShop.AuthWebApi.Services.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly IBus bus;

        public EmailSender(IBus bus)
        {
            this.bus = bus;
        }
        public async ValueTask SendAccountRegisteredMessage(AccountRegisteredMessage accountRegisteredMessage)
        {
            var queryName = "/account-registered";
            var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
            await endpoint.Send(accountRegisteredMessage);
        }

        public async ValueTask SendConfirmEmailMessage(ConfirmEmailMessage confirmEmailMessage)
        {
            var queryName = "/confirm-email";
            var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
            await endpoint.Send(confirmEmailMessage);
        }

        public async ValueTask SendResetPasswordMessage(ResetPasswordMessage resetPasswordMessage)
        {
            var queryName = "/reset-password";
            var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
            await endpoint.Send(resetPasswordMessage);
        }

        public async ValueTask SendTwoFactorAuthenticationCodeMessage(TwoFactorAuthenticationCodeMessage twoFactorAuthenticationCodeMessage)
        {
            var queryName = "/2fa-code";
            var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
            await endpoint.Send(twoFactorAuthenticationCodeMessage);
        }

        public async ValueTask SendAccountRegisteredOnExternalLoginMessage(AccountRegisteredOnExternalLoginMessage accountRegisteredOnExternalLoginMessage)
        {
            var queryName = "/registered-on-external-login";
            var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
            await endpoint.Send(accountRegisteredOnExternalLoginMessage);
        }

        public async ValueTask SendChangePhoneNumberMessage(ChangePhoneNumberMessage changePhoneNumberMessage)
        {
            var queryName = "/change-phone-number";
            var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
            await endpoint.Send(changePhoneNumberMessage);
        }

        private Uri CreateQueryUri(string queryName)
        {
            const string UriBase = "rabbitmq://localhost/";

            var Uri = new Uri(
                new StringBuilder(UriBase)
                .Append(queryName)
                .ToString());

            return Uri;
        }

        public async ValueTask SendChangeEmailMessage(ChangeEmailMessage changeEmailMessage)
        {
            var queryName = "/change-email";
            var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
            await endpoint.Send(changeEmailMessage);
        }
    }
}
