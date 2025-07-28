using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dkw.Abp;

[DependsOn(typeof(DkwModule))]
[DependsOn(typeof(DkwTestBaseModule))]
public class DkwTestsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DkwTestsModule>();
        });
    }
}
