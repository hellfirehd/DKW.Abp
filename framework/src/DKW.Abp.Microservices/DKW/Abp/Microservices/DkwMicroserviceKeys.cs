namespace Dkw.Abp.Microservices;

public static class DkwMicroserviceKeys
{
    public static String AspireRedis { get; set; } = DkwMicroserviceKeyDefaults.AspireRedis;
    public static String Authority { get; set; } = DkwMicroserviceKeyDefaults.Authority;
    public static String CorsOrigins { get; set; } = DkwMicroserviceKeyDefaults.CorsOrigins;
    public static String DataProtectionKey { get; set; } = DkwMicroserviceKeyDefaults.DataProtectionKey;
    public static String DistributedCacheKeyPrefix { get; set; } = DkwMicroserviceKeyDefaults.DistributedCacheKeyPrefix;
    public static String RedisConfiguration { get; set; } = DkwMicroserviceKeyDefaults.RedisConfiguration;
    public static String RequireHttpsMetadata { get; set; } = DkwMicroserviceKeyDefaults.RequireHttpsMetadata;
}
