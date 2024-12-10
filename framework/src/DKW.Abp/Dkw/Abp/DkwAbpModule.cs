using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DKW.Abp;

[DependsOn(typeof(AbpAutofacModule))]
public class DkwAbpModule : AbpModule;