using Dkw.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Security;

namespace Dkw.Abp.Security;

[DependsOn(typeof(AbpSecurityModule))]
[DependsOn(typeof(DkwModule))]
public class DkwSecurityModule : AbpModule
{
}
