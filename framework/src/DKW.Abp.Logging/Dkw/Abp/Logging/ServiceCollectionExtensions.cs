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
