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

using Dkw.Abp.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class MigrationExtensions
{
    public static async Task MigrateAsync(this IHost host, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(host);

        await host.Services.MigrateAsync(cancellationToken);
    }

    public static async Task MigrateAsync(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            await scope.MigrateAsync(cancellationToken);
        }
    }

    public static async Task MigrateAsync(this IServiceScope scope, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(scope);

        var migrationService = scope.ServiceProvider.GetRequiredService<IDbContextMigrationManager>();

        await migrationService.MigrateAsync(cancellationToken);
    }
}
