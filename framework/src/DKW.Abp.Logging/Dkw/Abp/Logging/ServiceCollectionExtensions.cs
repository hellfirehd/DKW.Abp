using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;

namespace DKW.Abp.Logging;

public static class ServiceCollectionExtensions
{
    public static LoggerConfiguration BuildDefaultLoggerConfiguration(this IServiceCollection services, LogEventLevel logEventLevel = LogEventLevel.Information)
    {
        var configuration = services.GetConfiguration();

        var loggingLevelSwitch = new LoggingLevelSwitch(logEventLevel);
        services.AddSingleton(loggingLevelSwitch);

        return new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.ControlledBy(loggingLevelSwitch)
            .Enrich.FromLogContext()
            .Enrich.WithProperty(EnricherPropertyNames.Group, GetApplicationGroup())
            .Enrich.WithProperty(EnricherPropertyNames.Application, services.GetApplicationName())
            .Enrich.WithProperty(EnricherPropertyNames.InstanceId, services.GetApplicationInstanceId())
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName();
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
