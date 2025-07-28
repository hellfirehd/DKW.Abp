using Dkw.Abp.Security;
using Volo.Abp.Modularity;

namespace Dkw.Abp.Logging;

[DependsOn(typeof(DkwSecurityModule))]
public class DkwLoggingModule : AbpModule;
