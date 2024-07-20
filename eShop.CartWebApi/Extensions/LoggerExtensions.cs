namespace eShop.CartWebApi.Extensions
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
    }
}
