namespace eShop.AuthApi.Services.Interfaces
{
    public interface IEmailSender
    {
        public ValueTask SendResetPasswordMessage(ResetPasswordMessage resetPasswordMessage);
        public ValueTask SendConfirmEmailMessage(ConfirmEmailMessage confirmEmailMessage);
        public ValueTask SendAccountRegisteredMessage(AccountRegisteredMessage accountRegisteredMessage);
        public ValueTask SendTwoFactorAuthenticationCodeMessage(TwoFactorAuthenticationCodeMessage twoFactorAuthenticationCodeMessage);
        public ValueTask SendAccountRegisteredOnExternalLoginMessage(AccountRegisteredOnExternalLoginMessage accountRegisteredOnExternalLoginMessage);
        public ValueTask SendChangeEmailMessage(ChangeEmailMessage changeEmailMessage);
        public ValueTask SendChangePhoneNumberMessage(ChangePhoneNumberMessage changePhoneNumberMessage);
    }
}
