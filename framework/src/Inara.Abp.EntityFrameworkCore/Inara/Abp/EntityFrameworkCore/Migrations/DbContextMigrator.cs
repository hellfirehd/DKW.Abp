using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.DependencyInjection;

namespace Inara.Abp.EntityFrameworkCore.Migrations;

public class DbContextMigrator<TDbContext>(IServiceProvider serviceProvider)
    : DbContextMigratorBase, IDbContextMigrator, ITransientDependency
    where TDbContext : DbContext
{
    private ILogger? _logger;
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    protected override ILogger Logger => _logger
        ??= ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger<TDbContext>()
        ?? new NullLogger<TDbContext>();

    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        await TryAsync(ApplyMigrations, cancellationToken: cancellationToken);
    }

    protected virtual async Task ApplyMigrations(CancellationToken cancellationToken = default)
    {
        var dbContext = ServiceProvider.GetRequiredService<TDbContext>();
        var name = dbContext.GetType().Name;
        var pendingMigrations = await dbContext
            .Database
            .GetPendingMigrationsAsync(cancellationToken: cancellationToken);

        if (pendingMigrations.Any())
        {
            Logger.LogInformation("Migrations Found: {Count}", pendingMigrations.Count());

            try
            {
                Logger.LogInformation("Applying Migrations: {DatabaseName}", name);

                var strategy = dbContext.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(dbContext.Database.MigrateAsync, cancellationToken);
                //await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Migrating {DatabaseName} failed: {Exception} - {Message}",
                    name, ex.GetType().Name, ex.Message);
                throw;
            }
        }
        else
        {
            Logger.LogInformation("No migrations to apply: {DatabaseName}", name);
        }
    }
}