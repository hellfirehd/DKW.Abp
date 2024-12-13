using Volo.Abp.Modularity;

namespace TestApp;

[DependsOn(typeof(TestAppApplicationModule))]
[DependsOn(typeof(TestAppDomainTestModule))]
public class TestAppApplicationTestModule : AbpModule
{

}
