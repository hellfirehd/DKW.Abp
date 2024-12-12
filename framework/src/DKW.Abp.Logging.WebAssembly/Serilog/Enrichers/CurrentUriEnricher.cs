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
