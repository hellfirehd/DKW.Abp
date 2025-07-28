using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Dkw.Abp;

public class DkwTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */

        return Task.CompletedTask;
    }
}
