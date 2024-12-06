namespace Inara.Abp;

public static class InaraKeys
{
    internal const String Key = "InaraAbp";

    public static class Configuration
    {
        public const String KeySize = "StringEncryption:KeySize";
        public const String DefaultPassPhrase = "StringEncryption:DefaultPassPhrase";
        public const String InitVectorBytes = "StringEncryption:InitVectorBytes";
        public const String DefaultSalt = "StringEncryption:DefaultSalt";
        public const String CorsAllowedOrigins = "Cors:AllowedOrigins";
    }
}