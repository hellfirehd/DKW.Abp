namespace Inara.Abp.EntityFrameworkCore.Migrations;

public interface IDatabaseMigrator
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}