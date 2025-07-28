// Canadian Professional Counsellors Association Application Suite
// Copyright (C) 2024 Doug Wilson
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

using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace Dkw.Abp.Logging.Enrichers;

[DebuggerStepThrough]
internal sealed class ConnectionDetailsEnricher(IHttpContextAccessor httpContextAccessor) : ILogEventEnricher
{
    private const String Property = "ClientAddress";
    private const String CacheKey = "RemoteIpAddressCache";

    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory factory)
    {
        var ctx = _httpContextAccessor.HttpContext;
        if (ctx == null)
        {
            return;
        }

        Add(ctx, factory, logEvent);
    }

    private static void Add(HttpContext ctx, ILogEventPropertyFactory factory, LogEvent logEvent)
    {
        var cached = ctx.Items[CacheKey] as String;
        if (cached == null)
        {
            cached = ctx.Connection.RemoteIpAddress?.ToString();
            ctx.Items[CacheKey] = String.IsNullOrWhiteSpace(cached) ? String.Empty : cached;
        }

        if (!String.IsNullOrWhiteSpace(cached))
        {
            logEvent.AddPropertyIfAbsent(factory.CreateProperty(Property, cached, false));
        }
    }
}
