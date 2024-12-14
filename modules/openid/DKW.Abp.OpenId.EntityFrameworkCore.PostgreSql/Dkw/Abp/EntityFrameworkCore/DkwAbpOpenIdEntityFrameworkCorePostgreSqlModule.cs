using DKW.Abp.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace DKW.Abp.EntityFrameworkCore;

[DependsOn(typeof(DkwAbpOpenIdEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
public class DkwAbpOpenIdEntityFrameworkCorePostgreSqlModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<DkwDbContextMigrationOptions>(options =>
        {
            // This looks like fun!
            options.Migrators.Add<OpenIddictMigrationDbContextMigrator>();
        });
    }
}
