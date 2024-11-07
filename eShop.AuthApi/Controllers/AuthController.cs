using eShop.AuthApi.Commands.Auth;
using eShop.AuthApi.Queries.Auth;
using eShop.Domain.Requests.Auth;
using Microsoft.AspNetCore.Authorization;

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

        [AllowAnonymous]
        [HttpPost("register")]
        public async ValueTask<ActionResult<ResponseDTO>> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var result = await sender.Send(new RegisterCommand(registrationRequest));

            return result.Match(
                succ => Ok(new ResponseBuilder().Succeeded().WithResultMessage(succ.Message).Build()),
                fail => ExceptionHandler.HandleException(fail));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async ValueTask<ActionResult<ResponseDTO>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await sender.Send(new LoginCommand(loginRequest));

            return result.Match(
                succ => Ok(new ResponseBuilder().Succeeded().WithResult(succ).WithResultMessage(succ.Message).Build()),
                fail => ExceptionHandler.HandleException(fail));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("change-personal-data")]
        public async ValueTask<ActionResult<ResponseDTO>> ChangePersonalData([FromBody] ChangePersonalDataRequest changePersonalDataRequest)
        {
            var result = await sender.Send(new ChangePersonalDataCommand(changePersonalDataRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage("Personal data was successfully changed.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpGet("get-personal-data/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetPersonalData(string Email)
        {
            var result = await sender.Send(new GetPersonalDataQuery(Email));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("change-password")]
        public async ValueTask<ActionResult<ResponseDTO>> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var result = await sender.Send(new ChangePasswordCommand(changePasswordRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [AllowAnonymous]
        [HttpPost("request-reset-password")]
        public async ValueTask<ActionResult<ResponseDTO>> ResetPasswordRequest(ResetPasswordRequest request)
        {
            var result = await sender.Send(new RequestResetPasswordCommand(request));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [AllowAnonymous]
        [HttpPost("confirm-reset-password")]
        public async ValueTask<ActionResult<ResponseDTO>> ConfirmResetPassword([FromBody] ConfirmResetPasswordRequest confirmPasswordResetRequest)
        {
            var result = await sender.Send(new ConfirmResetPasswordCommand(confirmPasswordResetRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [AllowAnonymous]
        [HttpPost("confirm-email")]
        public async ValueTask<ActionResult<ResponseDTO>> ConfirmEmail([FromBody] ConfirmEmailRequest confirmEmailRequest)
        {
            var result = await sender.Send(new ConfirmEmailCommand(confirmEmailRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("change-2fa-state")]
        public async ValueTask<ActionResult<ResponseDTO>> ChangeTwoFactorAuthentication([FromBody] ChangeTwoFactorAuthenticationRequest request)
        {
            var result = await sender.Send(new ChangeTwoFactorAuthenticationStateCommand(request));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpGet("get-2fa-state/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetTwoFactorAuthenticationState(string Email)
        {
            var result = await sender.Send(new GetTwoFactorAuthenticationStateQuery(Email));

            return result.Match(
                s => Ok(new ResponseBuilder()
                .Succeeded()
                .WithResultMessage(s.TwoFactorAuthenticationState
                    ? "Two factor authentication state is enabled."
                    : "Two factor authentication state is disabled.")
                .WithResult(s)
                .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [AllowAnonymous]
        [HttpPost("2fa-login")]
        public async ValueTask<ActionResult<ResponseDTO>> LoginWithTwoFactorAuthenticationCode([FromBody] TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest)
        {
            var result = await sender.Send(new TwoFactorAuthenticationLoginCommand(twoFactorAuthenticationLoginRequest));

            return result.Match(
                succ => Ok(new ResponseBuilder().Succeeded().WithResult(succ).WithResultMessage(succ.Message).Build()),
                fail => ExceptionHandler.HandleException(fail));
        }

        [AllowAnonymous]
        [HttpGet("external-login/{provider}")]
        public async ValueTask<ActionResult<ResponseDTO>> ExternalLogin(string provider, string? returnUri = null)
        {
            var result = await sender.Send(new ExternalLoginQuery(provider, returnUri));

            return result.Match(
                s => Challenge(s.AuthenticationProperties, s.Provider),
                f => ExceptionHandler.HandleException(f));
        }

        [AllowAnonymous]
        [HttpGet("handle-external-login-response")]
        public async ValueTask<ActionResult<ResponseDTO>> HandleExternalLoginResponse(string? remoteError = null, string? returnUri = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var result = await sender.Send(new HandleExternalLoginResponseQuery(info!, remoteError, returnUri));

            return result.Match(
                s => Redirect(s),
                f => ExceptionHandler.HandleException(f));
        }

        [AllowAnonymous]
        [HttpGet("get-external-providers")]
        public async ValueTask<ActionResult<ResponseDTO>> GetExternalProvidersList()
        {
            var result = await sender.Send(new GetExternalProvidersQuery());

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("request-change-email")]
        public async ValueTask<ActionResult<ResponseDTO>> RequestChangeEmail([FromBody] ChangeEmailRequest changeEmailRequest)
        {
            var result = await sender.Send(new ChangeEmailCommand(changeEmailRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("confirm-change-email")]
        public async ValueTask<ActionResult<ResponseDTO>> ConfirmChangeEmail([FromBody] ConfirmChangeEmailRequest confirmChangeEmailRequest)
        {
            var result = await sender.Send(new ConfirmChangeEmailCommand(confirmChangeEmailRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("change-user-name")]
        public async ValueTask<ActionResult<ResponseDTO>> ChangeUserName([FromBody] ChangeUserNameRequest changeUserNameRequest)
        {
            var result = await sender.Send(new ChangeUserNameCommand(changeUserNameRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("request-change-phone-number")]
        public async ValueTask<ActionResult<ResponseDTO>> RequestChangePhoneNumber([FromBody] ChangePhoneNumberRequest changePhoneNumberRequest)
        {
            var result = await sender.Send(new ChangePhoneNumberCommand(changePhoneNumberRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpPost("confirm-change-phone-number")]
        public async ValueTask<ActionResult<ResponseDTO>> ConfirmChangePhoneNumber([FromBody] ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest)
        {
            var result = await sender.Send(new ConfirmChangePhoneNumberCommand(confirmChangePhoneNumberRequest));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageAccountPolicy")]
        [HttpGet("get-phone-number/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetPhoneNumber(string Email)
        {
            var result = await sender.Send(new GetPhoneNumberQuery(Email));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}

