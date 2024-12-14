using TestApp.Samples;
using Xunit;

namespace TestApp.EntityFrameworkCore.Domains;

[Collection(TestAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TestAppEntityFrameworkCoreTestModule>
{

}
