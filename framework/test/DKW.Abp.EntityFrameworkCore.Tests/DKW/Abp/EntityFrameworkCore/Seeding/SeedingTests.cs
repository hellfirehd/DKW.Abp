using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using TestApp;
using TestApp.EntityFrameworkCore;
using TestApp.EntityFrameworkCore.Sqlite;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace DKW.Abp.EntityFrameworkCore.Seeding;

public class SeedingTests
{
    [Fact]
    public async Task SeedData()
    {
        var configuration = new ConfigurationBuilder().Build();

        using (var application = await AbpApplicationFactory.CreateAsync<TestAppMigrationModule>(options =>
        {
            options.Services.ReplaceConfiguration(configuration);
            options.UseAutofac();
        }))
        {
            await application.InitializeAsync();

            await application.ServiceProvider.SeedAsync();

            await application.ShutdownAsync();
        }
    }

    [DependsOn(typeof(AbpAspNetCoreAuthenticationJwtBearerModule))]
    [DependsOn(typeof(AbpAspNetCoreMvcUiMultiTenancyModule))]
    [DependsOn(typeof(AbpAspNetCoreSerilogModule))]
    [DependsOn(typeof(AbpAutofacModule))]
    [DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
    [DependsOn(typeof(AbpDistributedLockingModule))]
    [DependsOn(typeof(AbpSwashbuckleModule))]
    [DependsOn(typeof(TestAppApplicationModule))]
    [DependsOn(typeof(TestAppApplicationModule))]
    [DependsOn(typeof(TestAppEntityFrameworkCoreModule))]
    [DependsOn(typeof(TestAppEntityFrameworkCoreSqliteModule))]
    [DependsOn(typeof(TestAppHttpApiModule))]
    public class TestAppMigrationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddSingleton<IDistributedLockProvider>(sp =>
            {
                var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
                return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
            });
        }
    }
}
