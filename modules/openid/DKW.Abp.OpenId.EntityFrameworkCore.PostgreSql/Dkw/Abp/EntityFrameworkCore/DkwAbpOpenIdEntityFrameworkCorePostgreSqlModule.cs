using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace DKW.Abp.EntityFrameworkCore;

[DependsOn(typeof(DkwAbpOpenIdEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
public class DkwAbpOpenIdEntityFrameworkCorePostgreSqlModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql(options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));
        });
    }
}
