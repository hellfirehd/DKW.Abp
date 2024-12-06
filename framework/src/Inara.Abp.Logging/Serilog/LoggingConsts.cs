namespace Serilog;

public static class LoggingConsts
{
    public const String BootstrapTemplate = "[{Level:u3}] {Message:lj}{NewLine}{Exception}";
    public const String ConsoleTemplate = "[{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}";
}