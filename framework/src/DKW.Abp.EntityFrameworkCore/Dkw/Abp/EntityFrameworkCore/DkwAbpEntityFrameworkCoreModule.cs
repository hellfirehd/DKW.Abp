using DKW.Abp.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DKW.Abp.EntityFrameworkCore;

[DependsOn(typeof(AbpEntityFrameworkCoreModule))]
public class DkwAbpEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddDatabaseMigrators(context.Services);
    }

    private static void AutoAddDatabaseMigrators(IServiceCollection services)
    {
        var migrators = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IDbContextMigrator).IsAssignableFrom(context.ImplementationType))
            {
                migrators.Add(context.ImplementationType);
            }
        });

        services.Configure<DkwDbContextMigrationOptions>(options =>
        {
            // Add all found migrators
            options.Migrators.AddIfNotContains(migrators);
        });
    }
}
