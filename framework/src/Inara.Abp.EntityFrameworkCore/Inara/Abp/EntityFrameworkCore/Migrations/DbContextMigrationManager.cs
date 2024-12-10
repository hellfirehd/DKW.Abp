using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Inara.Abp.EntityFrameworkCore.Migrations;

public class DbContextMigrationManager(
        ILogger<DbContextMigrationManager> logger,
        IOptions<InaraDbContextMigrationOptions> options,
        IServiceScopeFactory serviceScopeFactory
    ) : ITransientDependency
{
    private readonly ILogger<DbContextMigrationManager> _logger = logger;
    private readonly InaraDbContextMigrationOptions _options = options.Value;

    public IServiceScopeFactory ServiceScopeFactory { get; } = serviceScopeFactory;

    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting migrations...");

        await MigrateDbContextAsync("Host", cancellationToken);

        _logger.LogInformation("Completed migrations.");
    }

    private async Task MigrateDbContextAsync(String name, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Migrating {Name} database...", name);
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            foreach (var migratorType in _options.Migrators)
            {
                var migrator = (IDbContextMigrator)scope.ServiceProvider.GetRequiredService(migratorType);

                await migrator.MigrateAsync(cancellationToken);
            }
        }
    }
}