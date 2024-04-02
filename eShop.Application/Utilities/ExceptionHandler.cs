using eShop.Domain.DTOs.Responses;
using eShop.Domain.Exceptions;
using eShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Application.Utilities
{
    public static class ExceptionHandler
    {
        public static ActionResult<ResponseDto> HandleException(Exception exception)
        {
            return exception switch
            {
                INotFoundException => new NotFoundObjectResult(new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(exception.Message)
                    .Build()),

                IBadRequestException => new BadRequestObjectResult(new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(exception.Message)
                    .Build()),

                IFailedValidationException => new BadRequestObjectResult(new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(exception.Message)
                    .AddErrors((exception as FailedValidationException)!.Errors.ToList())
                    .Build()),

                _ => new ObjectResult(new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(exception.Message)
                    .Build())
                { StatusCode = 500 }
            };
        }
    }
}
