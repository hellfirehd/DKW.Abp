namespace DKW.Abp.Logging;

public static class LoggingTemplateDefaults
{
    public const String BootstrapTemplate = "[{Level:u3}] {Message:lj}{NewLine}{Exception}";
    public const String BrowserConsole = "[{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}";
    public const String ConsoleTemplate = "[{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}";
    public const String DetailsTemplate = "Busness Rule Violation {Code}: {Details} {Data}";
    public const String NoDetailsTemplate = "Busness Rule Violation {Code}: {Data}";
}
