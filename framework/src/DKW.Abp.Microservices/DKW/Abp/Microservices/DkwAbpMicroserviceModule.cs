using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace DKW.Abp.Microservices;

[DependsOn(typeof(AbpAspNetCoreAuthenticationJwtBearerModule))]
[DependsOn(typeof(AbpAspNetCoreMvcUiMultiTenancyModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
[DependsOn(typeof(AbpDistributedLockingModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]
public class DkwAbpMicroserviceModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var redisConnStr = configuration[DkwAbpMicroserviceKeys.AspireRedis];
        configuration[DkwAbpMicroserviceKeys.RedisConfiguration] ??= redisConnStr ?? "localhost";
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        ConfigureCors(context, configuration);
        ConfigureDistributedLocking(context);

        context.Services.AddProblemDetails();
    }

    private static void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration[DkwAbpMicroserviceKeys.CorsOrigins]!
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    private static void ConfigureDistributedLocking(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = sp.GetService<IConnectionMultiplexer>()
                ?? ConnectionMultiplexer.Connect(context.Services.GetConfiguration()[DkwAbpMicroserviceKeys.RedisConfiguration]!);
            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
        });
    }
}
