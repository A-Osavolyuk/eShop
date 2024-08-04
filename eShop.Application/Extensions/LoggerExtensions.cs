using eShop.Domain.Common;
using eShop.Domain.Enums;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogInformation(this ILogger logger, string message, Guid RequestId)
        {
            logger.LogInformation($"{message}. Request Id: {RequestId}");
        }

        public static void LogError(this ILogger logger, string message, Guid RequestId)
        {
            logger.LogError($"{message}. Request Id: {RequestId}");
        }

        public static Result<TResponse> LogErrorWithException<TResponse>(this ILogger logger, Exception exception, ActionMessage actionMessage, Guid requestId)
        {
            logger.LogError("Failed to {action}: {message}. Request ID {requestId}", string.Format(actionMessage.Message, actionMessage.Args), exception.Message, requestId);

            return new(exception);
        }

        public static Result<TResponse> LogErrorWithException<TResponse>(this ILogger logger, Exception exception, ActionMessage actionMessage)
        {
            logger.LogError("Failed to {action}: {message}.", string.Format(actionMessage.Message, actionMessage.Args), exception.Message);

            return new(exception);
        }

        public static void LogError(this ILogger logger, Exception exception, ActionMessage actionMessage, Guid requestId)
        {
            logger.LogError("Failed to {action}: {message}. Request ID {requestId}", string.Format(actionMessage.Message, actionMessage.Args), exception.Message, requestId);
        }

        public static void LogError(this ILogger logger, Exception exception, ActionMessage actionMessage)
        {
            logger.LogError("Failed to {action}: {message}.", string.Format(actionMessage.Message, actionMessage.Args), exception.Message);
        }
    }
}
