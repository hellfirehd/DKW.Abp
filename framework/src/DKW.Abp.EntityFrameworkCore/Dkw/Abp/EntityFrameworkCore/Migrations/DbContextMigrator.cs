using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Dkw.Abp.EntityFrameworkCore.Migrations;

public class DbContextMigrator<TDbContext>(
    String databaseName,
    ICurrentTenant currentTenant,
    IAbpDistributedLock distributedLock,
    ILogger<DbContextMigrator<TDbContext>> logger,
    ITenantStore tenantStore,
    IUnitOfWorkManager unitOfWorkManager
    ) : IDbContextMigrator, ITransientDependency
    where TDbContext : DbContext, IEfCoreDbContext
{
    protected String DatabaseName { get; } = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
    protected ICurrentTenant CurrentTenant { get; } = currentTenant ?? throw new ArgumentNullException(nameof(currentTenant));
    protected IAbpDistributedLock DistributedLock { get; } = distributedLock ?? throw new ArgumentNullException(nameof(distributedLock));
    protected TimeSpan DistributedLockAcquireTimeout { get; set; } = TimeSpan.FromMinutes(15);
    protected ILogger<DbContextMigrator<TDbContext>> Logger { get; } = logger ?? throw new ArgumentNullException(nameof(logger));
    protected ITenantStore TenantStore { get; } = tenantStore ?? throw new ArgumentNullException(nameof(tenantStore));
    protected IUnitOfWorkManager UnitOfWorkManager { get; } = unitOfWorkManager ?? throw new ArgumentNullException(nameof(unitOfWorkManager));

    public virtual Task MigrateAsync(CancellationToken cancellationToken = default)
        => MigrateDatabaseSchemaAsync(null, cancellationToken);

    /// <summary>
    /// Apply pending EF Core schema migrations to the database.
    /// Returns true if any migration has applied.
    /// </summary>
    protected virtual async Task<Boolean> MigrateDatabaseSchemaAsync(Guid? tenantId, CancellationToken cancellationToken = default)
    {
        var result = false;

        using (CurrentTenant.Change(tenantId))
        {
            using (var uow = UnitOfWorkManager.Begin(requiresNew: true, isTransactional: false))
            {

                if (tenantId == null)
                {
                    //Migrating the host database
                    Logger.LogInformation("Migrating host database: {DatabaseName}", DatabaseName);
                    result = await MigrateDatabaseSchemaWithDbContextAsync(uow, cancellationToken);
                }
                else
                {
                    var tenantConfiguration = await TenantStore.FindAsync(tenantId.Value);
                    if (!tenantConfiguration!.ConnectionStrings!.Default.IsNullOrWhiteSpace() ||
                        !tenantConfiguration.ConnectionStrings.GetOrDefault(DatabaseName).IsNullOrWhiteSpace())
                    {
                        //Migrating the tenant database (only if tenant has a separate database)
                        Logger.LogInformation("Migrating separate database of tenant. Database Name: {DatabaseName}, TenantId: {tenantId}", DatabaseName, tenantId);

                        result = await MigrateDatabaseSchemaWithDbContextAsync(uow, cancellationToken);
                    }
                }

                await uow.CompleteAsync(cancellationToken);
            }
        }

        return result;
    }

    protected virtual async Task<Boolean> MigrateDatabaseSchemaWithDbContextAsync(IUnitOfWork uow, CancellationToken cancellationToken)
    {
        var unitOfWork = await uow.ServiceProvider
            .GetRequiredService<IDbContextProvider<TDbContext>>()
            .GetDbContextAsync();

        await using (await WaitForDistributedLockAsync(unitOfWork))
        {
            var pending = await unitOfWork.Database.GetPendingMigrationsAsync(cancellationToken) ?? [];

            Logger.LogInformation("Found {Count} pending migrations for database {DatabaseName}.", pending.Count(), DatabaseName);

            foreach (var migration in pending)
            {
                Logger.LogInformation("Pending Migration: {Migration}", migration);
            }

            var strategy = unitOfWork.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(unitOfWork.Database.MigrateAsync, cancellationToken);
        }

        return true;
    }

    protected virtual async Task<IAsyncDisposable> WaitForDistributedLockAsync(TDbContext unitOfWork)
    {
        var distributedLockHandle = await DistributedLock.TryAcquireAsync(
            "DatabaseMigrationEventHandler_" +
            unitOfWork.Database.GetConnectionString()!.ToUpperInvariant().ToMd5(),
            DistributedLockAcquireTimeout
        );

        return distributedLockHandle
            ?? throw new AbpException($"Distributed lock could not be acquired for database migration event handler: {DatabaseName}.");
    }
}
