using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DKW.Abp.Logging;

[DependsOn(typeof(AbpAutofacModule))]
public class DkwAbpLoggingModule : AbpModule;
