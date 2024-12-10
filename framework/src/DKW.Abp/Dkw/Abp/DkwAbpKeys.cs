namespace DKW.Abp;

public static class DkwAbpKeys
{
    internal const String Key = "DkwAbp";

    public static class Configuration
    {
        public const String KeySize = "StringEncryption:KeySize";
        public const String DefaultPassPhrase = "StringEncryption:DefaultPassPhrase";
        public const String InitVectorBytes = "StringEncryption:InitVectorBytes";
        public const String DefaultSalt = "StringEncryption:DefaultSalt";
        public const String CorsAllowedOrigins = "Cors:AllowedOrigins";
    }
}