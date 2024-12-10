using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Serilog;
using Serilog.Events;
using System.Globalization;
using Volo.Abp;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.Modularity;

namespace DKW.Abp.Logging;

[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
[DependsOn(typeof(DkwAbpLoggingModule))]
public class DkwAbpLoggingWebAssemblyModule : AbpModule
{
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(context.ServiceProvider.GetRequiredService<IConfiguration>())
            .WriteTo.BrowserConsole(
                restrictedToMinimumLevel: LogEventLevel.Information,
                outputTemplate: "[{Level:u3}] {Message:lj}{NewLine}{Exception}",
                CultureInfo.InvariantCulture,
                jsRuntime: context.ServiceProvider.GetRequiredService<IJSRuntime>())
            .CreateLogger();
    }
}