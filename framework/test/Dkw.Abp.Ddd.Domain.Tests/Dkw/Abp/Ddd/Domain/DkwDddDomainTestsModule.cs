using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Dkw.Abp.Ddd.Domain;

[DependsOn(typeof(DkwDddDomainModule))]
[DependsOn(typeof(DkwTestBaseModule))]
public class DkwDddDomainTestsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DkwDddDomainTestsModule>();
        });
    }
}
