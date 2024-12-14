using Volo.Abp.Modularity;

namespace TestApp;

public abstract class TestAppApplicationTestBase<TStartupModule> : TestAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
