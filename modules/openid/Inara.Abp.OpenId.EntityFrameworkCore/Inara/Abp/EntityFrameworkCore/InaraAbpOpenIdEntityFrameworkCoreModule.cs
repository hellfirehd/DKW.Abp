using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace Inara.Abp.EntityFrameworkCore;

[DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpOpenIddictEntityFrameworkCoreModule))]
[DependsOn(typeof(InaraAbpEntityFrameworkCoreModule))]
public class InaraAbpOpenIdEntityFrameworkCoreModule : AbpModule
{
}