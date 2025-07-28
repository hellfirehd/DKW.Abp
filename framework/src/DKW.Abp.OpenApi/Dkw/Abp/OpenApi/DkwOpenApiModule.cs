using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dkw.Abp.OpenApi;

[DependsOn(typeof(AbpAutofacModule))]
public class DkwOpenApiModule : AbpModule;
