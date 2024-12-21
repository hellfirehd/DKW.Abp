
namespace DKW.Abp.EntityFrameworkCore.Migrations;

public interface IDbContextMigrationManager
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}
