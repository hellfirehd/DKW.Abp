using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DKW.Abp.IdGeneration;

[DependsOn(typeof(AbpAutofacModule))]
public class DkwAbpIdGenerationApplicationModule : AbpModule;