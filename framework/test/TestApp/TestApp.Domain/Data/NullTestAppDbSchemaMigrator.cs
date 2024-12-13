using Volo.Abp.DependencyInjection;

namespace TestApp.Data;

/* This is used if database provider does't define
 * ITestAppDbSchemaMigrator implementation.
 */
public class NullTestAppDbSchemaMigrator : ITestAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
