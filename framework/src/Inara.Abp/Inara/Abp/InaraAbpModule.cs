using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Inara.Abp;

[DependsOn(typeof(AbpAutofacModule))]
public class InaraAbpModule : AbpModule;