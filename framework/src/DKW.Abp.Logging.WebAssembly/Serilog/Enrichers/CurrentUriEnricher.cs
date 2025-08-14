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

using Microsoft.AspNetCore.Components;
using Serilog.Configuration;
using Serilog.Core;
using Volo.Abp.DependencyInjection;

namespace Serilog.Enrichers;

/// <summary>
/// Enriches log events with a MachineName property containing <see cref="Environment.MachineName"/>.
/// </summary>
public class CurrentUriEnricher(IObjectAccessor<NavigationManager> accessor) : ILogEventEnricher
{
    private const String PropertyName = "URI";

    private readonly IObjectAccessor<NavigationManager> _accessor = accessor;

    /// <summary>
    /// Enrich the log event.
    /// </summary>
    /// <param name="logEvent">The log event to enrich.</param>
    /// <param name="factory">Factory for creating new properties to add to the event.</param>
    public void Enrich(Events.LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        if (_accessor.Value is not null)
        {
            logEvent.AddPropertyIfAbsent(factory.CreateProperty(PropertyName, _accessor.Value.Uri));
        }
    }
}

public static class CurrentLocationEnricherExtensions
{
    public static LoggerConfiguration WithCurrentLocation(this LoggerEnrichmentConfiguration enrichmentConfiguration, IObjectAccessor<NavigationManager> accessor)
        => enrichmentConfiguration.With(new CurrentUriEnricher(accessor));
}
