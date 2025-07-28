using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Reflection;

namespace Dkw.Abp.Logging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultLogger(this IServiceCollection services, Action<LoggerConfiguration>? configure = null)
    {
        var lls = new LoggingLevelSwitch();
        services.AddSingleton(lls);

        services.AddSerilog((provider, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(provider.GetRequiredService<IConfiguration>())
                .ReadFrom.Services(provider)
                .MinimumLevel.ControlledBy(lls)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.WithProperty(EnricherPropertyNames.Group, GetApplicationGroup())
                .Enrich.WithProperty(EnricherPropertyNames.Application, services.GetApplicationName())
                .Enrich.WithProperty(EnricherPropertyNames.InstanceId, services.GetApplicationInstanceId())
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.Console(outputTemplate: LoggingTemplates.ConsoleTemplate, theme: AnsiConsoleTheme.Code));

            configure?.Invoke(configuration);
        });

        Log.Information("Starting {ApplicationName} InstanceId: {InstanceId}",
            services.GetApplicationName(),
            services.GetApplicationInstanceId());
        Log.Debug("Debug Logging is Enabled");
        Log.Verbose("Verbose Logging is Enabled");

        return services;
    }

    private static String? GetApplicationGroup()
    {
        var name = Assembly.GetEntryAssembly()?.GetName().Name;

        if (!String.IsNullOrWhiteSpace(name))
        {
            var index = name.IndexOf('.');

            return index > 0 ? name[..index] : name;
        }

        return null;
    }
}
