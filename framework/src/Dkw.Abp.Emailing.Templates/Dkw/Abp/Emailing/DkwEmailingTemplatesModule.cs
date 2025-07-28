using Dkw.Abp.Templates;
using Volo.Abp.Modularity;

namespace Dkw.Abp.Emailing;

[DependsOn(typeof(DkwEmailingModule))]
[DependsOn(typeof(DkwTemplatesContractsModule))]
public class DkwEmailingTemplatesModule : AbpModule
{
}
