using eShop.Application.Utilities;
using eShop.Domain.DTOs.Requests.Auth;

namespace eShop.AuthWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(IAuthService authService, SignInManager<AppUser> signInManager)
        {
            this.authService = authService;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async ValueTask<ActionResult<ResponseDTO>> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var result = await authService.RegisterAsync(registrationRequest);

            return result.Match<ActionResult<ResponseDTO>>(
                succ =>
                {
                    return Ok(new ResponseBuilder()
                        .Succeeded()
                        .WithResultMessage(succ.Message)
                        .Build());
                },
                fail => ExceptionHandler.HandleException(fail));
        }

        [HttpPost("login")]
        public async ValueTask<ActionResult<ResponseDTO>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await authService.LoginAsync(loginRequest);

            return result.Match<ActionResult<ResponseDTO>>(
                succ =>
                {
                    return Ok(new ResponseBuilder()
                        .Succeeded()
                        .WithResult(succ)
                        .WithResultMessage(succ.Message)
                        .Build());
                },
                fail => ExceptionHandler.HandleException(fail));
        }

        [HttpPost("change-personal-data/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> ChangePersonalData([FromBody] ChangePersonalDataRequest changePersonalDataRequest, string Email)
        {
            var result = await authService.ChangePersonalDataAsync(Email, changePersonalDataRequest);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .WithResultMessage("Personal data was successfully changed.")
                    .WithResult(s)
                .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-personal-data/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetPersonalData(string Email)
        {
            var result = await authService.GetPersonalDataAsync(Email);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .WithResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("change-password/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest, string Email)
        {
            var result = await authService.ChangePasswordAsync(Email, changePasswordRequest);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .WithResultMessage(s.Message)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("request-reset-password/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> ResetPasswordRequest(string Email)
        {
            var result = await authService.RequestResetPasswordAsync(Email);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .WithResultMessage(s.Message)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("confirm-reset-password/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> ConfirmResetPassword(string Email, [FromBody] ConfirmPasswordResetRequest confirmPasswordResetRequest)
        {
            var result = await authService.ConfirmResetPasswordAsync(Email, confirmPasswordResetRequest);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .WithResultMessage(s.Message)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("confirm-email/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> ConfirmEmail(string Email, [FromBody] ConfirmEmailRequest confirmEmailRequest)
        {
            var result = await authService.ConfirmEmailAsync(Email, confirmEmailRequest);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .WithResultMessage("Your email address was successfully confirmed.")
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("change-2fa-state/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> ChangeTwoFactorAuthentication(string Email)
        {
            var result = await authService.ChangeTwoFactorAuthenticationStateAsync(Email);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                .Succeeded()
                .WithResultMessage(s.Message)
                .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-2fa-state/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetTwoFactorAuthenticationState(string Email)
        {
            var result = await authService.GetTwoFactorAuthenticationStateAsync(Email);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                .Succeeded()
                .WithResultMessage(s.TwoFactorAuthenticationState
                    ? "Two factor authentication state is enabled."
                    : "Two factor authentication state is disabled.")
                .WithResult(s)
                .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("2fa-login/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> LoginWithTwoFactorAuthenticationCode(string Email, [FromBody] TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest)
        {
            var result = await authService.LoginWithTwoFactorAuthenticationCodeAsync(Email, twoFactorAuthenticationLoginRequest);

            return result.Match<ActionResult<ResponseDTO>>(
                succ =>
                {
                    return Ok(new ResponseBuilder()
                        .Succeeded()
                        .WithResult(succ)
                        .WithResultMessage(succ.Message)
                        .Build());
                },
                fail => ExceptionHandler.HandleException(fail));
        }

        [HttpGet("external-login/{provider}")]
        public async ValueTask<ActionResult<ResponseDTO>> ExternalLogin(string provider, string? returnUri = null)
        {
            var result = await authService.RequestExternalLogin(provider, returnUri);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Challenge(s.AuthenticationProperties, s.Provider),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("handle-external-login-response")]
        public async ValueTask<ActionResult<ResponseDTO>> HandleExternalLoginResponse(string? remoteError = null, string? returnUri = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var result = await authService.HandleExternalLoginResponseAsync(info!, returnUri ?? "/");

            return result.Match<ActionResult<ResponseDTO>>(
                s => Redirect(s),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-external-providers")]
        public async ValueTask<ActionResult<ResponseDTO>> GetExternalProvidersList()
        {
            var result = await authService.GetExternalProvidersAsync();

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("request-change-email")]
        public async ValueTask<ActionResult<ResponseDTO>> RequestChangeEmail([FromBody]ChangeEmailRequest changeEmailRequest)
        {
            var result = await authService.RequestChangeEmailAsync(changeEmailRequest);

            return result.Match<ActionResult<ResponseDTO>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .WithResultMessage(s.Message)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("confirm-change-email")]
        public async ValueTask<ActionResult<ResponseDTO>> ConfirmChangeEmail([FromBody] ConfirmChangeEmailRequest confirmChangeEmailRequest)
        {
            var result = await authService.ConfirmChangeEmailAsync(confirmChangeEmailRequest);

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("change-user-name")]
        public async ValueTask<ActionResult<ResponseDTO>> ChangeUserName([FromBody] ChangeUserNameRequest changeUserNameRequest)
        {
            var result = await authService.ChangeUserNameAsync(changeUserNameRequest);

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("refresh-token")]
        public ActionResult<ResponseDTO> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest) 
        { 
            var result = authService.RefreshTokenAsync(refreshTokenRequest);

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("request-change-phone-number")]
        public async ValueTask<ActionResult<ResponseDTO>> RequestChangePhoneNumber([FromBody] ChangePhoneNumberRequest changePhoneNumberRequest)
        {
            var result = await authService.RequestChangePhoneNumberAsync(changePhoneNumberRequest);

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("confirm-change-phone-number")]
        public async ValueTask<ActionResult<ResponseDTO>> ConfirmChangePhoneNumber([FromBody] ConfirmChangePhoneNumberRequest confirmChangePhoneNumberRequest)
        {
            var result = await authService.ConfirmChangePhoneNumberAsync(confirmChangePhoneNumberRequest);

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-phone-number/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetPhoneNumber(string Email)
        {
            var result = await authService.GetPhoneNumber(Email);

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}

