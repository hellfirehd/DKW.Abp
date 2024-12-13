using DKW.Abp.TestBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace DKW.Abp.EntityFrameworkCore;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqliteModule))]
[DependsOn(typeof(AbpTestBaseModule))]
[DependsOn(typeof(DkwAbpEntityFrameworkCoreModule))]
[DependsOn(typeof(DkwAbpTestBaseModule))]
public class DkwAbpEntityFrameworkCoreTestsModule : AbpModule
{
}

/// <summary>
/// Avoid unit test caching the configure of the entity.
/// OnModelCreating will be executed multiple times
/// </summary>
public class UnitTestModelCacheKeyFactory : IModelCacheKeyFactory
{
    public Object Create(DbContext context)
    {
        return context;
    }

    public virtual Object Create(DbContext context, Boolean designTime)
    {
        return context;
    }
}
