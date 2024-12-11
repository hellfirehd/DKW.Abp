namespace DKW.Abp.Microservices;

public static class DkwAbpMicroserviceKeys
{
    public static String AspireRedis { get; set; } = DkwAbpMicroserviceKeyDefaults.AspireRedis;
    public static String Authority { get; set; } = DkwAbpMicroserviceKeyDefaults.Authority;
    public static String CorsOrigins { get; set; } = DkwAbpMicroserviceKeyDefaults.CorsOrigins;
    public static String DataProtectionKey { get; set; } = DkwAbpMicroserviceKeyDefaults.DataProtectionKey;
    public static String DistributedCacheKeyPrefix { get; set; } = DkwAbpMicroserviceKeyDefaults.DistributedCacheKeyPrefix;
    public static String RedisConfiguration { get; set; } = DkwAbpMicroserviceKeyDefaults.RedisConfiguration;
    public static String RequireHttpsMetadata { get; set; } = DkwAbpMicroserviceKeyDefaults.RequireHttpsMetadata;
}
