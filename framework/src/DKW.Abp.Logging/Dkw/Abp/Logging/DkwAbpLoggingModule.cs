using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace DKW.Abp.Logging;

[DependsOn(typeof(AbpAutofacModule))]
public class DkwAbpLoggingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        Log.Logger = context.Services.BuildDefaultLoggerConfiguration().CreateLogger();

        context.Services.AddLogging(builder => builder.ClearProviders().AddSerilog());

        Log.Information("Starting {ApplicationName} InstanceId: {InstanceId}",
            context.Services.GetApplicationName(),
            context.Services.GetApplicationInstanceId());

        Log.Debug("Debug Logging is Enabled");
        Log.Verbose("Verbose Logging is Enabled");
    }
}
