using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;

namespace DKW.Abp.Microservices;

public static class ServiceConfigurationContextExtensions
{
    public static ServiceConfigurationContext ConfigureMicroservice(this ServiceConfigurationContext context, String name)
    {
        context.ConfigureAuthentication(name);
        context.ConfigureCache(name);
        context.ConfigureDataProtection(name);
        context.ConfigureSwaggerServices(name);

        return context;
    }

    public static ServiceConfigurationContext ConfigureAuthentication(this ServiceConfigurationContext context, String audience)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddAbpJwtBearer(options =>
            {
                options.Authority = configuration[DkwAbpMicroserviceKeys.Authority]
                    ?? throw new DkwAbpException(DkwAbpErrorCodes.MissingConfigurationValue, DkwAbpMicroserviceKeys.Authority);
                options.RequireHttpsMetadata = configuration.GetValue<Boolean>(DkwAbpMicroserviceKeys.RequireHttpsMetadata);
                options.Audience = audience;
            });

        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });

        return context;
    }

    public static ServiceConfigurationContext ConfigureCache(this ServiceConfigurationContext context, String keyPrefix)
    {
        context.Services.Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = $"{keyPrefix}:";
        });

        return context;
    }

    public static void ConfigureDataProtection(this ServiceConfigurationContext context, String name)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName(name);
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration[DkwAbpMicroserviceKeys.RedisConfiguration] ?? "redis");
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, DkwAbpMicroserviceKeys.DataProtectionKey);
        }
    }

    public static ServiceConfigurationContext ConfigureSwaggerServices(this ServiceConfigurationContext context, String name, String version = "v1")
    {
        var configuration = context.Services.GetConfiguration();
        var authority = configuration[DkwAbpMicroserviceKeys.Authority]
            ?? throw new DkwAbpException(DkwAbpErrorCodes.MissingConfigurationValue, DkwAbpMicroserviceKeys.Authority);

        context.Services.AddAbpSwaggerGenWithOAuth(
            authority,
            new Dictionary<String, String> {
                {name, $"{name} API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = $"{name} API", Version = version });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        return context;
    }
}
