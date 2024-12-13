using TestApp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace TestApp.Blazor.Client;

public abstract class TestAppComponentBase : AbpComponentBase
{
    protected TestAppComponentBase()
    {
        LocalizationResource = typeof(TestAppResource);
    }
}
