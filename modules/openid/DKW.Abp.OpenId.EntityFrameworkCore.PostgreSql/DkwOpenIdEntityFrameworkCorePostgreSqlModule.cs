using Dkw.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Dkw.Abp.OpenId.EntityFrameworkCore.PostgreSql;

[DependsOn(typeof(DkwOpenIdEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCorePostgreSqlModule))]
public class DkwOpenIdEntityFrameworkCorePostgreSqlModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql(options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));
        });
    }
}
