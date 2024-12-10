using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Serilog;

public static class LoggingServiceCollectionExtensions
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services, Action<LoggerConfiguration>? config = null)
    {
        var configuration = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("OpenIddict", LogEventLevel.Information)
            .MinimumLevel.Override("Quartz", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName();
#if DEBUG
        configuration.WriteTo.Console(outputTemplate: LoggingConsts.ConsoleTemplate, theme: AnsiConsoleTheme.Literate);
#endif
        config?.Invoke(configuration);

        services.AddSerilog(configuration.CreateLogger(), true);

        return services;
    }
}