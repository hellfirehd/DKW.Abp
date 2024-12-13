using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace DKW.Abp.EntityFrameworkCore;

[DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpOpenIddictEntityFrameworkCoreModule))]
[DependsOn(typeof(DkwAbpEntityFrameworkCoreModule))]
public class DkwAbpOpenIdEntityFrameworkCoreModule : AbpModule
{
}
