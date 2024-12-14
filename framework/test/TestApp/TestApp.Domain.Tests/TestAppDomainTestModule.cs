using Volo.Abp.Modularity;

namespace TestApp;

[DependsOn(typeof(TestAppDomainModule))]
[DependsOn(typeof(TestAppTestBaseModule))]
public class TestAppDomainTestModule : AbpModule
{

}
