using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using Volo.Abp.Modularity;

using static DKW.Abp.Microservices.ConfigKeys;

namespace DKW.Abp.Microservices;

public static class ServiceConfigurationContextExtensions
{
    public static void ConfigureDataProtection(this ServiceConfigurationContext context, String name)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName(name);
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration[RedisConfiguration] ?? "redis");
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, DataProtectionKey);
        }
    }
}