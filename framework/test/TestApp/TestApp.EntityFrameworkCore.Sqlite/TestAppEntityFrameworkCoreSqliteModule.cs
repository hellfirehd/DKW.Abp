using DKW.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace TestApp.EntityFrameworkCore.Sqlite;

[DependsOn(typeof(AbpEntityFrameworkCoreSqliteModule))]
[DependsOn(typeof(DkwAbpEntityFrameworkCoreModule))]
[DependsOn(typeof(TestAppEntityFrameworkCoreModule))]
public class TestAppEntityFrameworkCoreSqliteModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlite(sql =>
            {
                sql.MigrationsAssembly(GetType().Assembly.GetName().Name);
            });
        });
    }
}
