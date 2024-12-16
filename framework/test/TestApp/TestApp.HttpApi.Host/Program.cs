using DKW.Abp.Logging;
using Serilog;

namespace TestApp;

public class Program
{
    public async static Task<Int32> Main(String[] args)
    {
        Log.Logger = new LoggerConfiguration()
           .WriteTo.Async(c => c.Console(outputTemplate: LoggingTemplates.BootstrapTemplate))
           .CreateBootstrapLogger();

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac();

            var abp = await builder.AddApplicationAsync<TestAppHttpApiHostModule>();

            var app = builder.Build();

            await app.InitializeApplicationAsync();

            await app.MigrateAsync();
            await app.SeedAsync();

            await app.RunAsync();

            return 0;
        }
        catch (Exception ex) when (ex is not HostAbortedException)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}
