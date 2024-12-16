using DKW.Abp.EntityFrameworkCore;
using DKW.Abp.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Volo.Abp.DistributedLocking;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace DKW.Abp.OpenId.EntityFrameworkCore.PostgreSql;

public class OpenIdDbContextMigrator(ICurrentTenant currentTenant, IAbpDistributedLock distributedLock, ILogger<DbContextMigrator<OpenIdDbContext>> logger, ITenantStore tenantStore, IUnitOfWorkManager unitOfWorkManager)
        : DbContextMigrator<OpenIdDbContext>(DbConsts.ConnectionStringName, currentTenant, distributedLock, logger, tenantStore, unitOfWorkManager)
{
}
