using Inara.Abp.Logging;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using StackExchange.Redis;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

using static Inara.Abp.Microservices.ConfigKeys;

namespace Inara.Abp.Microservices;

[DependsOn(typeof(AbpAspNetCoreMvcUiMultiTenancyModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
[DependsOn(typeof(AbpDistributedLockingModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]
[DependsOn(typeof(InaraAbpLoggingModule))]
public class InaraAbpMicroserviceModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var redisConnStr = configuration[AspireRedis];
        configuration[RedisConfiguration] ??= redisConnStr ?? "localhost";
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureLogging(context, configuration);
        ConfigureCache();
        ConfigureDataProtection(context, configuration, hostingEnvironment);
        ConfigureDistributedLocking(context);

        context.Services.AddProblemDetails();
    }

    private static void ConfigureLogging(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.ConfigureLogging(config => config.ReadFrom.Configuration(configuration));
    }

    private void ConfigureCache()
    {
        Configure<AbpDistributedCacheOptions>(options => options.KeyPrefix = DistributedCacheKeyPrefix);
    }

    private static void ConfigureDataProtection(
        ServiceConfigurationContext context,
        IConfiguration configuration,
        IWebHostEnvironment hostingEnvironment)
    {
        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Inara");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration[RedisConfiguration] ?? "redis");
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, DataProtectionKey);
        }
    }

    private static void ConfigureDistributedLocking(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = sp.GetRequiredService<IConnectionMultiplexer>();
            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
        });
    }
}