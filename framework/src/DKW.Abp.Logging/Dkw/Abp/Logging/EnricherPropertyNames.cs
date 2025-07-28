namespace Dkw.Abp.Logging;

public static class EnricherPropertyNames
{
    public static String Application { get; set; } = EnricherPropertyNameDefaults.ApplicationFieldName;
    public static String Group { get; set; } = EnricherPropertyNameDefaults.GroupFieldName;
    public static String InstanceId { get; set; } = EnricherPropertyNameDefaults.InstanceIdFieldName;
}
