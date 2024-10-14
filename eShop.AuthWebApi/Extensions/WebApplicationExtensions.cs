using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace eShop.AuthWebApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task ConfigureDatabaseAsync<TDbContext>(this WebApplication app) where TDbContext : DbContext
        {
            using var score = app.Services.CreateScope();
            var context = score.ServiceProvider.GetRequiredService<TDbContext>();
            await EnsureDatabaseAsync(context);
            await RunMigrationsAsync(context);
        }

        private static async Task EnsureDatabaseAsync(DbContext context)
        {
            var dbCreator = context.GetService<IRelationalDatabaseCreator>();
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                if (!await dbCreator.ExistsAsync())
                {
                    await context.Database.EnsureCreatedAsync();
                }
            });

        }

        private static async Task RunMigrationsAsync(DbContext context)
        {
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await context.Database.BeginTransactionAsync();
                await context.Database.MigrateAsync();
                await transaction.CommitAsync();
            });
        }
    }
}
