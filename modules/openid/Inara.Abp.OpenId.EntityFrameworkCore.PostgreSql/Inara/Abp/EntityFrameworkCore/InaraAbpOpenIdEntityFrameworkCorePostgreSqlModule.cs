using Inara.Abp.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Inara.Abp.EntityFrameworkCore;

[DependsOn(typeof(InaraAbpOpenIdEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
public class InaraAbpOpenIdEntityFrameworkCorePostgreSqlModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<InaraDbContextMigrationOptions>(options =>
        {
            // This looks like fun!
            options.Migrators.Add<OpenIddictMigrationDbContextMigrator>();
        });
    }
}