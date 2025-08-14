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

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Microsoft.Extensions.DependencyInjection;

public static class DkwLogging
{
    public const String ConsoleTemplate = "[{Timestamp:HH:mm:ss}:{Level:u3}] {RequestId} {SourceContext} | {Message:lj}{NewLine}{Exception}";
    public const String BootstrapTemplate = "[{Timestamp:HH:mm:ss}:{Level:u3}] {Message:lj}{NewLine}{Exception}";

    public static LoggerConfiguration DefaultConfiguration
        => new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("OpenIddict", LogEventLevel.Information)
            .MinimumLevel.Override("Quartz", LogEventLevel.Warning);

    public static void Intialize<T>()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: BootstrapTemplate, theme: AnsiConsoleTheme.Literate, formatProvider: CultureInfo.InvariantCulture)
            .CreateBootstrapLogger();

        var assemblyName = typeof(T).Assembly.GetName().Name ?? "UnknownAssembly";
        Log.Information("Starting {Application} ...", assemblyName);
    }
}
