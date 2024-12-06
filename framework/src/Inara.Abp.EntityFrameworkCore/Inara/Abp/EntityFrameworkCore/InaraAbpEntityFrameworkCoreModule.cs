using Inara.Abp.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Inara.Abp.EntityFrameworkCore;

[DependsOn(typeof(AbpEntityFrameworkCoreModule))]
public class InaraAbpEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AutoAddDatabaseMigrators(context.Services);
    }

    private static void AutoAddDatabaseMigrators(IServiceCollection services)
    {
        var contributors = new List<Type>();

        services.OnRegistered(context =>
        {
            if (typeof(IDatabaseMigrator).IsAssignableFrom(context.ImplementationType))
            {
                contributors.Add(context.ImplementationType);
            }
        });

        services.Configure<InaraMigrationOptions>(options =>
        {
            // Add all found contributors
            options.Migrators.AddIfNotContains(contributors);
        });
    }
}