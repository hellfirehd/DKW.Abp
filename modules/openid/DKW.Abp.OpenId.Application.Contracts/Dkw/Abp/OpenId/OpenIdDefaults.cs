namespace Dkw.Abp.OpenId;

public static class OpenIdDefaults
{
    public const String ApplicationsKey = "OpenId:Applications";
    public const String ScopesKey = "OpenId:Scopes";

    public static String AppSettingsFilename { get; set; } = "appsettings.{0}.endpoints.json";
}
