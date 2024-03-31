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
                fail =>
                {
                    if (fail is FailedValidationException validationException)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(validationException.Message)
                            .AddErrors(validationException.Errors.ToList())
                            .Build());

                    if (fail is NullReferenceException referenceException)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(referenceException.Message)
                            .Build());

                    if (fail is InvalidRegisterAttemptException identityException)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(identityException!.ErrorType)
                            .AddErrors(identityException.Errors.ToList())
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(fail.Message)
                        .Build());
                });
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
                fail =>
                {
                    if (fail is NotFoundUserByEmailException notFoundUserByEmail)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserByEmail.Message)
                            .Build());

                    if (fail is NullRequestException nullRequest)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(nullRequest.Message)
                            .Build());

                    if (fail is FailedValidationException validationException)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(validationException.Message)
                            .AddErrors(validationException.Errors.ToList())
                            .Build());

                    if (fail is InvalidLoginAttemptException loginException)
                        return BadRequest(new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(loginException!.Message)
                        .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(fail.Message)
                        .Build());

                });
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
                f =>
                {
                    if (f is FailedValidationException failedValidationException)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(failedValidationException.Message)
                            .AddErrors(failedValidationException.Errors.ToList())
                            .Build());

                    if (f is NotFoundUserByIdException notFoundUserException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserException.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                f =>
                {
                    if (f is NotFoundUserByIdException notFoundUserException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserException.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                f =>
                {
                    if (f is FailedValidationException failedValidationException)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(failedValidationException.Message)
                            .AddErrors(failedValidationException.Errors.ToList())
                            .Build());

                    if (f is WrongPasswordException wrongPasswordException)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(wrongPasswordException.Message)
                            .Build());

                    if (f is NotFoundUserByIdException notFoundUserException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserException.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                f =>
                {
                    if (f is NotFoundUserByEmailException notFoundUserException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserException.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                f =>
                {
                    if (f is NotFoundUserByIdException notFoundUserException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserException.Message)
                            .Build());

                    if (f is FailedValidationException failedValidationException)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(failedValidationException.Message)
                            .AddErrors(failedValidationException.Errors.ToList())
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                f =>
                {
                    if (f is NotFoundUserByEmailException notFoundUserByEmailException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserByEmailException.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                f =>
                {
                    if (f is NotFoundUserByEmailException notFoundUserByEmailException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserByEmailException.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                f =>
                {
                    if (f is NotFoundUserByEmailException notFoundUserByEmailException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserByEmailException.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                fail =>
                {
                    if (fail is NotFoundUserByEmailException notFoundUserByEmail)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserByEmail.Message)
                            .Build());

                    if (fail is InvalidTwoFactorAuthenticationCodeException invalidTwoFactorAuthenticationCodeException)
                        return BadRequest(new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(invalidTwoFactorAuthenticationCodeException!.Message)
                        .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(fail.Message)
                        .Build());

                });
        }

        [HttpGet("external-login/{provider}")]
        public async ValueTask<ActionResult<ResponseDto>> ExternalLogin(string provider, string? returnUri = null)
        {
            var result = await authService.RequestExternalLogin(provider, returnUri);

            return result.Match<ActionResult<ResponseDto>>(
                s => Challenge(s.AuthenticationProperties, s.Provider),
                f => StatusCode(500, new ResponseBuilder().Failed().AddErrorMessage(f.Message).Build()));
        }

        [HttpGet("handle-external-login-response")]
        public async ValueTask<ActionResult<ResponseDto>> HandleExternalLoginResponse(string? remoteError = null, string? returnUri = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var result = await authService.HandleExternalLoginResponseAsync(info!, returnUri ?? "/");

            return result.Match<ActionResult>(
                s => Redirect(s),
                f => StatusCode(500, new ResponseBuilder().Failed().AddErrorMessage(f.Message).Build()));
        }

        [HttpGet("get-external-providers")]
        public async ValueTask<ActionResult<ResponseDto>> GetExternalProvidersList()
        {
            var result = await authService.GetExternalProviders();

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder().Succeeded().AddResult(s).Build()),
                f => StatusCode(500, new ResponseBuilder().Failed().AddErrorMessage(f.Message).Build()));
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
                f =>
                {
                    if (f is NotFoundUserByEmailException notFoundUserByEmailException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(notFoundUserByEmailException.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
        }
    }
}
