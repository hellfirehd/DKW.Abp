using DKW.Abp.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace TestApp.EntityFrameworkCore.Sqlite;

public class TestAppDbContextMigrator(
    ICurrentTenant currentTenant,
    IAbpDistributedLock distributedLock,
    ILogger<DbContextMigrator<TestAppDbContext>> logger,
    ITenantStore tenantStore,
    IUnitOfWorkManager unitOfWorkManager
    ) : DbContextMigrator<TestAppDbContext>("TestApp", currentTenant, distributedLock, logger, tenantStore, unitOfWorkManager)
{
}
