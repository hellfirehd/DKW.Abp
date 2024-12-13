using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestApp;
using TestApp.EntityFrameworkCore;
using TestApp.EntityFrameworkCore.Sqlite;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Tokens;
using Volo.Abp.Swashbuckle;

namespace DKW.Abp.EntityFrameworkCore;

public class MigrationAndSeedingTests
{
    [Fact]
    public async Task ApplyMigrations()
    {
        var configuration = new ConfigurationBuilder().Build();

        using (var application = await AbpApplicationFactory.CreateAsync<TestAppMigrationModule>(options =>
        {
            options.Services.ReplaceConfiguration(configuration);
            options.UseAutofac();
        }))
        {
            await application.InitializeAsync();

            var migrator = application.ServiceProvider.GetRequiredService<TestAppDbContextMigrator>();

            await application.ServiceProvider.MigrateAsync();
            await application.ServiceProvider.SeedAsync();

            await application.ShutdownAsync();
        }
    }

    [DependsOn(typeof(AbpAspNetCoreAuthenticationJwtBearerModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcUiMultiTenancyModule))]
    [DependsOn(typeof(AbpAspNetCoreSerilogModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
    [DependsOn(typeof(AbpDistributedLockingAbstractionsModule))]
    [DependsOn(typeof(AbpSwashbuckleModule))]
    [DependsOn(typeof(TestAppApplicationModule))]
    [DependsOn(typeof(TestAppEntityFrameworkCoreModule))]
    [DependsOn(typeof(TestAppEntityFrameworkCoreSqliteModule))]
    [DependsOn(typeof(TestAppHttpApiModule))]
    public class TestAppMigrationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<TokenCleanupOptions>(options =>
            {
                options.IsCleanupEnabled = false;
            });
        }
    }
}
