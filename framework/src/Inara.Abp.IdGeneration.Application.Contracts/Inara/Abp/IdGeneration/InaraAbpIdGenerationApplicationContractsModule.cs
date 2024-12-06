using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Inara.Abp.IdGeneration;

[DependsOn(typeof(AbpAutofacModule))]
public class InaraAbpIdGenerationApplicationContractsModule : AbpModule;