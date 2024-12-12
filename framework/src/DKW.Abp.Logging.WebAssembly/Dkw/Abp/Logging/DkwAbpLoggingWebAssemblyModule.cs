using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using System.Globalization;
using Volo.Abp;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;

namespace DKW.Abp.Logging;

[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
public class DkwAbpLoggingWebAssemblyModule : AbpModule
{
    private LoggerConfiguration loggerConfiguration = new();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddObjectAccessor<NavigationManager>();
        loggerConfiguration = context.Services.BuildDefaultLoggerConfiguration();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        Log.Logger = loggerConfiguration
            .Enrich.WithCurrentLocation(context.ServiceProvider.GetRequiredService<IObjectAccessor<NavigationManager>>())
            .WriteTo.BrowserConsole(
                restrictedToMinimumLevel: LogEventLevel.Information,
                outputTemplate: LoggingTemplates.BrowserConsole,
                CultureInfo.InvariantCulture,
                jsRuntime: context.ServiceProvider.GetRequiredService<IJSRuntime>())
            .CreateLogger();
    }
}
