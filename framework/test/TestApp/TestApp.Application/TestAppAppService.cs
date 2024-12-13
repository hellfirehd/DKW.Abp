using TestApp.Localization;
using Volo.Abp.Application.Services;

namespace TestApp;

/* Inherit your application services from this class.
 */
public abstract class TestAppAppService : ApplicationService
{
    protected TestAppAppService()
    {
        LocalizationResource = typeof(TestAppResource);
    }
}
