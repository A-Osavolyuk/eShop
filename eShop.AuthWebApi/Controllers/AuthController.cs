using eShop.AuthWebApi.Services.Interfaces;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace eShop.AuthWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async ValueTask<ActionResult<ResponseDto>> Register([FromBody] RegistrationRequestDto registrationRequest)
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

                    var identityException = fail as InvalidRegisterAttemptException;
                    return BadRequest(new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(identityException!.ErrorType)
                        .AddErrors(identityException.Errors.ToList())
                        .Build());
                });
        }

        [HttpPost("login")]
        public async ValueTask<ActionResult<ResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            var result = await authService.LoginAsync(loginRequest);

            return result.Match<ActionResult<ResponseDto>>(
                succ =>
                {
                    return Ok(new ResponseBuilder()
                        .Succeeded()
                        .AddResult(succ)
                        .AddResultMessage("Successfully logged id.")
                        .Build());
                },
                fail =>
                {
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

                    var loginException = fail as InvalidLoginAttemptException;
                    return BadRequest(new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(loginException!.Message)
                        .Build());
                });
        }
    }
}
