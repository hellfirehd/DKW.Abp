using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DKW.Abp.OpenApi;

[DependsOn(typeof(AbpAutofacModule))]
public class DkwAbpOpenApiModule : AbpModule;
