using eShop.Application.Utilities;
using LanguageExt.Pretty;

namespace eShop.AuthWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IAuthService authService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            this.authService = authService;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost("register")]
        public async ValueTask<ActionResult<ResponseDto>> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var result = await authService.RegisterAsync(registrationRequest);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    return Ok(new ResponseBuilder()
                        .Succeeded()
                        .AddResultMessage(succ.Message)
                        .Build());
                },
                fail => ExceptionHandler.HandleException(fail));
        }

        [HttpPost("login")]
        public async ValueTask<ActionResult<ResponseDto>> Login([FromBody] LoginRequest loginRequest)
        {
            var result = await authService.LoginAsync(loginRequest);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    return Ok(new ResponseBuilder()
                        .Succeeded()
                        .AddResult(succ)
                        .AddResultMessage(succ.Message)
                        .Build());
                },
                fail => ExceptionHandler.HandleException(fail));
        }

        [HttpPost("change-personal-data/{Id}")]
        public async ValueTask<ActionResult<ResponseDto>> ChangePersonalData([FromBody] ChangePersonalDataRequest changePersonalDataRequest, string Id)
        {
            var result = await authService.ChangePersonalDataAsync(Id, changePersonalDataRequest);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Personal data was successfully changed.")
                    .AddResult(s)
                .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-personal-data/{Id}")]
        public async ValueTask<ActionResult<ResponseDto>> GetPersonalData(string Id)
        {
            var result = await authService.GetPersonalDataAsync(Id);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("change-password/{Id}")]
        public async ValueTask<ActionResult<ResponseDto>> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest, string Id)
        {
            var result = await authService.ChangePasswordAsync(Id, changePasswordRequest);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage(s.Message)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("request-reset-password/{Email}")]
        public async ValueTask<ActionResult<ResponseDto>> ResetPasswordRequest(string Email)
        {
            var result = await authService.RequestResetPasswordAsync(Email);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage(s.Message)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("confirm-reset-password/{Email}")]
        public async ValueTask<ActionResult<ResponseDto>> ConfirmResetPassword(string Email, [FromBody] ConfirmPasswordResetRequest confirmPasswordResetRequest)
        {
            var result = await authService.ConfirmResetPasswordAsync(Email, confirmPasswordResetRequest);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage(s.Message)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("confirm-email/{Email}")]
        public async ValueTask<ActionResult<ResponseDto>> ConfirmEmail(string Email, [FromBody] ConfirmEmailRequest confirmEmailRequest)
        {
            var result = await authService.ConfirmEmailAsync(Email, confirmEmailRequest);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Your email address was successfully confirmed.")
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("change-2fa-state/{Email}")]
        public async ValueTask<ActionResult<ResponseDto>> ChangeTwoFactorAuthentication(string Email)
        {
            var result = await authService.ChangeTwoFactorAuthenticationStateAsync(Email);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                .Succeeded()
                .AddResultMessage(s.Message)
                .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-2fa-state/{Email}")]
        public async ValueTask<ActionResult<ResponseDto>> GetTwoFactorAuthenticationState(string Email)
        {
            var result = await authService.GetTwoFactorAuthenticationStateAsync(Email);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                .Succeeded()
                .AddResultMessage(s.TwoFactorAuthenticationState
                    ? "Two factor authentication state is enabled."
                    : "Two factor authentication state is disabled.")
                .AddResult(s)
                .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("2fa-login/{Email}")]
        public async ValueTask<ActionResult<ResponseDto>> LoginWithTwoFactorAuthenticationCode(string Email, TwoFactorAuthenticationLoginRequest twoFactorAuthenticationLoginRequest)
        {
            var result = await authService.LoginWithTwoFactorAuthenticationCodeAsync(Email, twoFactorAuthenticationLoginRequest);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    return Ok(new ResponseBuilder()
                        .Succeeded()
                        .AddResult(succ)
                        .AddResultMessage(succ.Message)
                        .Build());
                },
                fail => ExceptionHandler.HandleException(f));
        }

        [HttpGet("external-login/{provider}")]
        public async ValueTask<ActionResult<ResponseDto>> ExternalLogin(string provider, string? returnUri = null)
        {
            var result = await authService.RequestExternalLogin(provider, returnUri);

            return result.Match<ActionResult<ResponseDto>>(
                s => Challenge(s.AuthenticationProperties, s.Provider),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("handle-external-login-response")]
        public async ValueTask<ActionResult<ResponseDto>> HandleExternalLoginResponse(string? remoteError = null, string? returnUri = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var result = await authService.HandleExternalLoginResponseAsync(info!, returnUri ?? "/");

            return result.Match<ActionResult<ResponseDto>>(
                s => Redirect(s),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-external-providers")]
        public async ValueTask<ActionResult<ResponseDto>> GetExternalProvidersList()
        {
            var result = await authService.GetExternalProviders();

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder().Succeeded().AddResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("request-change-email")]
        public async ValueTask<ActionResult<ResponseDto>> RequestChangeEmail(ChangeEmailRequest changeEmailRequest)
        {
            var result = await authService.RequestChangeEmailAsync(changeEmailRequest);

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .AddResultMessage(s.Message)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
