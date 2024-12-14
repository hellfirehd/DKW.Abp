using Xunit;

namespace TestApp.EntityFrameworkCore;

[CollectionDefinition(TestAppTestConsts.CollectionDefinitionName)]
public class TestAppEntityFrameworkCoreCollection : ICollectionFixture<TestAppEntityFrameworkCoreFixture>
{

}
