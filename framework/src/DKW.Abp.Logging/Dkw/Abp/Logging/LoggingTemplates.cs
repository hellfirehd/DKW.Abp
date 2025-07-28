namespace Dkw.Abp.Logging;

public static class LoggingTemplates
{
    public static String BootstrapTemplate { get; set; } = LoggingTemplateDefaults.BootstrapTemplate;
    public static String ConsoleTemplate { get; set; } = LoggingTemplateDefaults.ConsoleTemplate;
    public static String DetailsTemplate { get; set; } = LoggingTemplateDefaults.DetailsTemplate;
    public static String NoDetailsTemplate { get; set; } = LoggingTemplateDefaults.NoDetailsTemplate;
    public static String BrowserConsole { get; set; } = LoggingTemplateDefaults.BrowserConsole;
}
