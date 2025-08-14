// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

using Dkw.Abp.Logging;
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
