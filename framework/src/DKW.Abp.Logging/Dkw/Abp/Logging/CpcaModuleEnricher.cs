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
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;
using Volo.Abp.Modularity;

namespace Dkw.Abp.Logging;

[DebuggerStepThrough]
internal sealed class DkwModuleEnricher(ILoggedModule module) : ILogEventEnricher
{
    private readonly ILoggedModule _module = module;
    private LogEventProperty? _application;
    private LogEventProperty? _instance;
    private LogEventProperty? _version;

    /// <summary>
    /// Enrich the log event.
    /// </summary>
    /// <param name="logEvent">The log event to enrich.</param>
    /// <param name="factory">Factory for creating new properties to add to the event.</param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        if (_module is not null)
        {
            logEvent.AddPropertyIfAbsent(_application ??= _application = factory.CreateProperty(LoggingFields.Application, _module.Application));
            logEvent.AddPropertyIfAbsent(_instance ??= factory.CreateProperty(LoggingFields.Instance, _module.Instance));
            logEvent.AddPropertyIfAbsent(_version ??= factory.CreateProperty(LoggingFields.Version, _module.Version));
        }
    }
}

public static partial class LoggerEnrichmentConfigurationExtensions
{
    public static LoggerConfiguration WithLoggedModule(this LoggerEnrichmentConfiguration enrichmentConfiguration, ILoggedModule module)
        => enrichmentConfiguration.With(new DkwModuleEnricher(module));
}
