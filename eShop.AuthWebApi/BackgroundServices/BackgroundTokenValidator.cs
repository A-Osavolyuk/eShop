
namespace eShop.AuthWebApi.BackgroundServices
{
    public class BackgroundTokenValidator(
        IServiceScopeFactory scopeFactory,
        ILogger<BackgroundTokenValidator> logger) : IHostedService, IDisposable
    {
        private Timer? timer;
        private readonly IServiceScopeFactory scopeFactory = scopeFactory;
        private readonly ILogger<BackgroundTokenValidator> logger = logger;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(async state => await ValidateTokensAsync(), null, TimeSpan.FromMinutes(5), TimeSpan.FromHours(12));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async Task ValidateTokensAsync()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

                var userTokens = await context.UserAuthenticationTokens.AsNoTracking().ToListAsync();

                foreach (var userToken in userTokens) 
                { 
                    if(userToken.ExpiredAt >= DateTime.UtcNow)
                    {
                        context.UserAuthenticationTokens.Remove(userToken);
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
