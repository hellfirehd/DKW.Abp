using TestApp.Samples;
using Xunit;

namespace TestApp.EntityFrameworkCore.Applications;

[Collection(TestAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TestAppEntityFrameworkCoreTestModule>
{

}
