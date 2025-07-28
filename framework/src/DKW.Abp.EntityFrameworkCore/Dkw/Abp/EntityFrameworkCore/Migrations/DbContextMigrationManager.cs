using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Dkw.Abp.EntityFrameworkCore.Migrations;

public class DbContextMigrationManager(
        ILogger<DbContextMigrationManager> logger,
        IOptions<DkwDbContextMigrationOptions> options,
        IServiceScopeFactory serviceScopeFactory
    ) : IDbContextMigrationManager, ITransientDependency
{
    private readonly ILogger<DbContextMigrationManager> _logger = logger;
    private readonly DkwDbContextMigrationOptions _options = options.Value;

    public IServiceScopeFactory ServiceScopeFactory { get; } = serviceScopeFactory;

    public virtual async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting migrations...");

        await MigrateDbContextAsync(cancellationToken);

        _logger.LogInformation("Completed migrations.");
    }

    private async Task MigrateDbContextAsync(CancellationToken cancellationToken = default)
    {
        using (var scope = ServiceScopeFactory.CreateScope())
        {
            foreach (var migratorType in _options.Migrators)
            {
                _logger.LogInformation("Migrating {Name} ...", migratorType.Name);
                var migrator = (IDbContextMigrator)scope.ServiceProvider.GetRequiredService(migratorType);

                try
                {
                    await migrator.MigrateAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                    throw;
                }
            }
        }
    }
}
