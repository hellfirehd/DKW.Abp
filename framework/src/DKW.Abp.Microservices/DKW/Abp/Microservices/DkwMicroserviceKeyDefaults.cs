namespace Dkw.Abp.Microservices;

public static class DkwMicroserviceKeyDefaults
{
    public const String AspireRedis = "ConnectionStrings:Redis";
    public const String Authority = "AuthServer:Authority";
    public const String CorsOrigins = "App:CorsOrigins";
    public const String DataProtectionKey = "DKW-Protection-Keys";
    public const String DistributedCacheKeyPrefix = "DKW:";
    public const String RedisConfiguration = "Redis:Configuration";
    public const String RequireHttpsMetadata = "AuthServer:RequireHttpsMetadata";
}
