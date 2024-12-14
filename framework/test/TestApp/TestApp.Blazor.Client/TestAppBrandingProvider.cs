using Microsoft.Extensions.Localization;
using TestApp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace TestApp.Blazor.Client;

[Dependency(ReplaceServices = true)]
public class TestAppBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TestAppResource> _localizer;

    public TestAppBrandingProvider(IStringLocalizer<TestAppResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
