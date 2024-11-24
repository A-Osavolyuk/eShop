namespace eShop.AuthApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class AuthController(SignInManager<AppUser> signInManager, ISender sender) : ControllerBase
    {
        private readonly SignInManager<AppUser> signInManager = signInManager;
        private readonly ISender sender = sender;

        #region Get methods

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpGet("get-personal-data/{email}")]
        public async ValueTask<ActionResult<ResponseDto>> GetPersonalData(string email)
        {
            var result = await sender.Send(new GetPersonalDataQuery(email));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpGet("get-2fa-state/{email}")]
        public async ValueTask<ActionResult<ResponseDto>> GetTwoFactorAuthenticationState(string email)
        {
            var result = await sender.Send(new GetTwoFactorAuthenticationStateQuery(email));

            return result.Match(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .WithResultMessage(s.TwoFactorAuthenticationState
                        ? "Two factor authentication state is enabled."
                        : "Two factor authentication state is disabled.")
                    .WithResult(s)
                    .Build()),
                ExceptionHandler.HandleException);
        }

        [AllowAnonymous]
        [HttpGet("external-login/{provider}")]
        public async ValueTask<ActionResult<ResponseDto>> ExternalLogin(string provider, string? returnUri = null)
        {
            var result = await sender.Send(new ExternalLoginQuery(provider, returnUri));

            return result.Match(
                s => Challenge(s.AuthenticationProperties, s.Provider),
                ExceptionHandler.HandleException);
        }

        [AllowAnonymous]
        [HttpGet("handle-external-login-response")]
        public async ValueTask<ActionResult<ResponseDto>> HandleExternalLoginResponse(string? remoteError = null,
            string? returnUri = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var result = await sender.Send(new HandleExternalLoginResponseQuery(info!, remoteError, returnUri));

            return result.Match(
                s => Redirect(s),
                ExceptionHandler.HandleException);
        }

        [AllowAnonymous]
        [HttpGet("get-external-providers")]
        public async ValueTask<ActionResult<ResponseDto>> GetExternalProvidersList()
        {
            var result = await sender.Send(new GetExternalProvidersQuery());

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpGet("get-phone-number/{email}")]
        public async ValueTask<ActionResult<ResponseDto>> GetPhoneNumber(string email)
        {
            var result = await sender.Send(new GetPhoneNumberQuery(email));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }

        #endregion

        #region Post methods

        [AllowAnonymous]
        [HttpPost("register")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var result = await sender.Send(new RegisterCommand(registrationRequest));

            return result.Match(
                succ => Ok(new ResponseBuilder().Succeeded().WithResultMessage(succ.Message).Build()),
                ExceptionHandler.HandleException);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await sender.Send(new LoginCommand(loginRequest));

            return result.Match(
                succ => Ok(new ResponseBuilder().Succeeded().WithResult(succ).WithResultMessage(succ.Message).Build()),
                ExceptionHandler.HandleException);
        }
        
        [AllowAnonymous]
        [HttpPost("request-reset-password")]
        public async ValueTask<ActionResult<ResponseDto>> ResetPasswordRequest(ResetPasswordRequest request)
        {
            var result = await sender.Send(new RequestResetPasswordCommand(request));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
        

        [AllowAnonymous]
        [HttpPost("confirm-email")]
        public async ValueTask<ActionResult<ResponseDto>> ConfirmEmail(
            [FromBody] ConfirmEmailRequest confirmEmailRequest)
        {
            var result = await sender.Send(new ConfirmEmailCommand(confirmEmailRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("change-2fa-state")]
        public async ValueTask<ActionResult<ResponseDto>> ChangeTwoFactorAuthentication(
            [FromBody] ChangeTwoFactorAuthenticationRequest request)
        {
            var result = await sender.Send(new ChangeTwoFactorAuthenticationStateCommand(request));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }

        [AllowAnonymous]
        [HttpPost("2fa-login")]
        public async ValueTask<ActionResult<ResponseDto>> LoginWithTwoFactorAuthenticationCode(
            [FromBody] TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest)
        {
            var result =
                await sender.Send(new TwoFactorAuthenticationLoginCommand(twoFactorAuthenticationLoginRequest));

            return result.Match(
                succ => Ok(new ResponseBuilder().Succeeded().WithResult(succ).WithResultMessage(succ.Message).Build()),
                ExceptionHandler.HandleException);
        }
        
        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("confirm-change-email")]
        public async ValueTask<ActionResult<ResponseDto>> ConfirmChangeEmail(
            [FromBody] ConfirmChangeEmailRequest confirmChangeEmailRequest)
        {
            var result = await sender.Send(new ConfirmChangeEmailCommand(confirmChangeEmailRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
        
        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("confirm-change-phone-number")]
        public async ValueTask<ActionResult<ResponseDto>> ConfirmChangePhoneNumber(
            [FromBody] ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest)
        {
            var result = await sender.Send(new ConfirmChangePhoneNumberCommand(confirmChangePhoneNumberRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }

        #endregion

        #region Put methods

        [AllowAnonymous]
        [HttpPut("confirm-reset-password")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> ConfirmResetPassword(
            [FromBody] ConfirmResetPasswordRequest confirmPasswordResetRequest)
        {
            var result = await sender.Send(new ConfirmResetPasswordCommand(confirmPasswordResetRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
        
        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPut("request-change-email")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> RequestChangeEmail(
            [FromBody] ChangeEmailRequest changeEmailRequest)
        {
            var result = await sender.Send(new ChangeEmailCommand(changeEmailRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPut("change-user-name")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> ChangeUserName(
            [FromBody] ChangeUserNameRequest changeUserNameRequest)
        {
            var result = await sender.Send(new ChangeUserNameCommand(changeUserNameRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPut("request-change-phone-number")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> RequestChangePhoneNumber(
            [FromBody] ChangePhoneNumberRequest changePhoneNumberRequest)
        {
            var result = await sender.Send(new ChangePhoneNumberCommand(changePhoneNumberRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPut("change-personal-data")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> ChangePersonalData(
            [FromBody] ChangePersonalDataRequest changePersonalDataRequest)
        {
            var result = await sender.Send(new ChangePersonalDataCommand(changePersonalDataRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage("Personal data was successfully changed.")
                    .WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPut("change-password")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> ChangePassword(
            [FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var result = await sender.Send(new ChangePasswordCommand(changePasswordRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }

        #endregion
    }
}