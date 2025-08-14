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

using Dkw.Abp.Security;
using OpenIddict.Abstractions;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using System.Diagnostics;
using Volo.Abp.Security.Claims;

namespace Dkw.Abp.Logging;

/// <summary>
/// Enriches log events with a MachineName property containing <see cref="Environment.MachineName"/>.
/// </summary>
[DebuggerNonUserCode]
public class CurrentPrincipalEnricher(ICurrentPrincipalAccessor accessor) : ILogEventEnricher
{
    private readonly ICurrentPrincipalAccessor _accessor = accessor;

    /// <summary>
    /// Enrich the log event.
    /// </summary>
    /// <param name="logEvent">The log event to enrich.</param>
    /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
    [DebuggerStepThrough]
    public void Enrich(Serilog.Events.LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var user = _accessor.Principal;
        if (user is not null)
        {
            var username = user.GetClaim(AbpClaimTypes.UserName);
            if (!String.IsNullOrWhiteSpace(username))
            {
                logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(LoggingFields.UserId, username));
            }

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(LoggingFields.CurrentUser, user.FullName()));
        }
    }
}

public static partial class LoggerEnrichmentConfigurationExtensions
{
    public static LoggerConfiguration WithCurrentPrincipal(this LoggerEnrichmentConfiguration enrichmentConfiguration, ICurrentPrincipalAccessor userAccessor)
        => enrichmentConfiguration.With(new CurrentPrincipalEnricher(userAccessor));
}
