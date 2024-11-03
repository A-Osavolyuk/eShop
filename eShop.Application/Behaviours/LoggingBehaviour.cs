using System.Reflection;
using eShop.Domain.Exceptions;
using eShop.Domain.Interfaces;
using LanguageExt.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Unit = LanguageExt.Unit;

namespace eShop.Application.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestId = Guid.NewGuid();
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling request {name}. Request ID {requestId}.",
            requestName, requestId);

        var response = await next();
        var exception = GetException(response);

        if (exception is null)
        {
            switch (exception)
            {
                case INotFoundException or IBadRequestException or IFailedValidationException:
                    logger.LogInformation("{message}. Request ID {requestId}.", exception.Message, requestId);
                    break;
                case IInternalServerError:
                    logger.LogError("{message}. {description}. Request ID {requestId}.", exception.Message, exception.InnerException,
                        requestId);
                    break;
                default: 
                    logger.LogError("{message}. {description}. Request ID {requestId}.", exception?.Message, exception?.InnerException,
                        requestId);
                    break;
            }
        }
        else
        {
            logger.LogInformation(
                "Successfully handled request {name}. Request ID {requestId}.", requestName, requestId);
        }
        
        return response;
    }

    private Exception? GetException(TResponse response)
    {
        var field = response?.GetType().GetField("exception", BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
        var value = field?.GetValue(response);
        return value as Exception;
    }
}