using eShop.Domain.Common;
using LanguageExt.Common;

namespace eShop.Application.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogInformation(this ILogger logger, string message, Guid requestId)
        {
            logger.LogInformation($"{message}. Request Id: {requestId}");
        }

        public static void LogError(this ILogger logger, string message, Guid requestId)
        {
            logger.LogError($"{message}. Request Id: {requestId}");
        }
    }
}
