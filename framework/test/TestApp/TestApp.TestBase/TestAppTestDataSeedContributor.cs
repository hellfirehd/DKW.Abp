﻿using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace TestApp;

public class TestAppTestDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    public Task SeedAsync(DataSeedContext context)
    {
        /* Seed additional test data... */

        return Task.CompletedTask;
    }
}
