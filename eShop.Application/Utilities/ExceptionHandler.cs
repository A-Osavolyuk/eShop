using eShop.Domain.DTOs;
using eShop.Domain.Exceptions;
using eShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Application.Utilities
{
    public static class ExceptionHandler
    {
        public static ActionResult HandleException(Exception exception)
        {
            return exception switch
            {
                INotFoundException => new NotFoundObjectResult(new ResponseBuilder()
                    .Failed()
                    .WithErrorMessage(exception.Message)
                    .Build()),

                IBadRequestException => new BadRequestObjectResult(new ResponseBuilder()
                    .Failed()
                    .WithErrorMessage(exception.Message)
                    .Build()),

                IFailedValidationException => new BadRequestObjectResult(new ResponseBuilder()
                    .Failed()
                    .WithErrorMessage(exception.Message)
                    .WithErrors((exception as FailedValidationException)!.Errors.ToList())
                    .Build()),

                _ => new ObjectResult(new ResponseBuilder()
                    .Failed()
                    .WithErrorMessage(exception.Message)
                    .Build())
                { StatusCode = 500 }
            };
        }
    }
}
