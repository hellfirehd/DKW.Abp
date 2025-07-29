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
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using System.Diagnostics;

namespace Dkw.Abp.Logging.Enrichers;

public class RequestHeadersEnricher(IHttpContextAccessor httpContextAccessor) : ILogEventEnricher
{
    private const String UserAgent = "UserAgent";
    private const String ClientVersion = "ClientVersion";
    private const String ClientInstance = "ClientInstance";
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    [DebuggerStepThrough]
    public void Enrich(Serilog.Events.LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var ctx = _httpContextAccessor.HttpContext;
        if (ctx == null)
        {
            return;
        }

        Add(ctx, propertyFactory, logEvent, ClientInstance, LogHeaders.ClientInstanceHeader);
        Add(ctx, propertyFactory, logEvent, ClientVersion, LogHeaders.ClientVersionHeader);
        Add(ctx, propertyFactory, logEvent, UserAgent, LogHeaders.UserAgent);
    }

    private static void Add(HttpContext ctx, ILogEventPropertyFactory factory, Serilog.Events.LogEvent logEvent, String property, String header)
    {
        var cached = ctx.Items[header] as String;
        if (cached == null)
        {
            if (ctx.Request.Headers.TryGetValue(header, out var value))
            {
                cached = value.FirstOrDefault();
                ctx.Items[header] = cached;
            }
        }

        if (!String.IsNullOrWhiteSpace(cached))
        {
            logEvent.AddPropertyIfAbsent(factory.CreateProperty(property, cached, false));
        }
    }
}

public static class HttpContextEnricherExtensions
{
    public static LoggerConfiguration WithRequestHeaders(this LoggerEnrichmentConfiguration enrichmentConfiguration, IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        var provider = services.BuildServiceProvider();
        var accessor = provider.GetRequiredService<IHttpContextAccessor>();

        return enrichmentConfiguration.With(new RequestHeadersEnricher(accessor));
    }
}
