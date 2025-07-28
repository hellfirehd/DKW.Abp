using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dkw.Abp.Ddd.Domain;

[DependsOn(typeof(DkwModule))]
[DependsOn(typeof(DkwDddModule))]
[DependsOn(typeof(AbpDddDomainModule))]
public class DkwDddDomainModule : AbpModule
{
}
