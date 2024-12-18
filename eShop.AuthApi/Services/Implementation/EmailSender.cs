namespace eShop.AuthApi.Services.Implementation;

internal sealed class EmailSender(IBus bus) : IEmailSender
{
    private readonly IBus bus = bus;

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
        const string uriBase = "rabbitmq://localhost/";

        var uri = new Uri(
            new StringBuilder(uriBase)
                .Append(queryName)
                .ToString());

        return uri;
    }

    public async ValueTask SendChangeEmailMessage(ChangeEmailMessage changeEmailMessage)
    {
        var queryName = "/change-email";
        var endpoint = await bus.GetSendEndpoint(CreateQueryUri(queryName));
        await endpoint.Send(changeEmailMessage);
    }
}