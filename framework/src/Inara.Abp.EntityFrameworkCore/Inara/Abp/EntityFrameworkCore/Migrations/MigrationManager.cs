using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Inara.Abp.EntityFrameworkCore.Migrations;

public class MigrationManager(
        ILogger<MigrationManager> logger,
        IOptions<InaraMigrationOptions> options,
        IServiceScopeFactory serviceScopeFactory
    ) : ITransientDependency
{
    private readonly ILogger<MigrationManager> _logger = logger;
    private readonly InaraMigrationOptions _options = options.Value;

    public IServiceScopeFactory ServiceScopeFactory { get; } = serviceScopeFactory;

    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting migrations...");

        await MigrateDatabaseAsync("Host", cancellationToken);

        _logger.LogInformation("Completed migrations.");
    }

    private async Task MigrateDatabaseAsync(String name, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Migrating {Name} database...", name);
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            foreach (var migratorType in _options.Migrators)
            {
                var migrator = (IDatabaseMigrator)scope.ServiceProvider.GetRequiredService(migratorType);

                await migrator.MigrateAsync(cancellationToken);
            }
        }
    }
}