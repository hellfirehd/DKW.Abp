namespace DKW.Abp.EntityFrameworkCore.Migrations;

public interface IDbContextMigrator
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}