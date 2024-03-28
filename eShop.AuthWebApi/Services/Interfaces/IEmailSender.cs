namespace eShop.AuthWebApi.Services.Interfaces
{
    public interface IEmailSender
    {
        public ValueTask SendResetPasswordMessage(ResetPasswordMessage resetPasswordMessage);
        public ValueTask SendConfirmEmailMessage(ConfirmEmailMessage confirmEmailMessage);
        public ValueTask SendAccountRegisteredMessage(AccountRegisteredMessage accountRegisteredMessage);
        public ValueTask SendTwoFactorAuthenticationCodeMessage(TwoFactorAuthenticationCodeMessage twoFactorAuthenticationCodeMessage);
    }
}
