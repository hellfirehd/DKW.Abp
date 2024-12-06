using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Inara.Abp.OpenApi;

[DependsOn(typeof(AbpAutofacModule))]
public class InaraAbpOpenApiModule : AbpModule;