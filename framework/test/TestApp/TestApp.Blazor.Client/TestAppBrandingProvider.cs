using Microsoft.Extensions.Localization;
using TestApp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace TestApp.Blazor.Client;

[Dependency(ReplaceServices = true)]
public class TestAppBrandingProvider(IStringLocalizer<TestAppResource> localizer) : DefaultBrandingProvider
{
    private readonly IStringLocalizer<TestAppResource> _localizer = localizer;

    public override String AppName => _localizer["AppName"];
}
