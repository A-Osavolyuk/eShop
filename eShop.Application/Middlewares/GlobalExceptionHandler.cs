using eShop.Domain.DTOs;
using eShop.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Middlewares;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling global exception");
        
        if (exception is FailedValidationException failedValidationException)
        {
            logger.LogInformation("Handled validation exception");
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(
                new ResponseBuilder()
                    .Failed()
                    .WithErrorMessage(failedValidationException.Message)
                    .WithErrors(failedValidationException.Errors.ToList())
                    .Build(), cancellationToken);
        }

        return true;
    }
}