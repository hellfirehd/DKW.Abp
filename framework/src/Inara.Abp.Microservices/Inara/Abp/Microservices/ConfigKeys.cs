namespace Inara.Abp.Microservices;

public static class ConfigKeys
{
    public const String AspireRedis = "ConnectionStrings:Redis";
    public const String RedisConfiguration = "Redis:Configuration";
    public const String DistributedCacheKeyPrefix = "Inara:";
    public const String DataProtectionKey = "Inara-Protection-Keys";
}