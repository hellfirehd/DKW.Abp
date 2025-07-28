using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dkw.Abp.Emailing;

[DependsOn(typeof(DkwEmailingModule))]
[DependsOn(typeof(DkwTestBaseModule))]
public class DkwEmailingTestsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DkwEmailingTestsModule>();
        });
    }
}
