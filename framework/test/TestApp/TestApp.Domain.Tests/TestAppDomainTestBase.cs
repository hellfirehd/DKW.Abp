using Volo.Abp.Modularity;

namespace TestApp;

/* Inherit from this class for your domain layer tests. */
public abstract class TestAppDomainTestBase<TStartupModule> : TestAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
