using eShop.AuthWebApi.Services.Interfaces;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Exceptions;
using eShop.Domain.Exceptions.Auth;
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

        [HttpPost("change-personal-data/{Id}/{Token}")]
        public async ValueTask<ActionResult<ResponseDto>> ChangePersonalData([FromBody] ChangePersonalDataRequestDto changePersonalDataRequest, string Id, string Token)
        {
            var result = await authService.ChangePersonalInformation(Id, Token, changePersonalDataRequest);

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

                    if (f is NotFoundUserException notFoundUserException)
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
    }
}
