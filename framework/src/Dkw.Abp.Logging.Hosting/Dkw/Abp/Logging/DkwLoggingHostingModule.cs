using Volo.Abp.Modularity;

namespace Dkw.Abp.Logging;

[DependsOn(typeof(DkwLoggingModule))]
public class DkwLoggingHostingModule : AbpModule
{
}
