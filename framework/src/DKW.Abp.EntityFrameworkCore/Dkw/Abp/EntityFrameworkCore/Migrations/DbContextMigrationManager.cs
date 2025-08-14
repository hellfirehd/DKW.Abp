// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

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
