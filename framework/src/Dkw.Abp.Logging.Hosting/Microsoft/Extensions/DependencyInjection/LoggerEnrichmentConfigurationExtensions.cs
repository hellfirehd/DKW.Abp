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

using Dkw.Abp.Logging.Enrichers;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class LoggerEnrichmentConfigurationExtensions
{
    public static LoggerConfiguration WithRequestHeaders(this LoggerEnrichmentConfiguration enrichmentConfiguration, IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        var provider = services.BuildServiceProvider();
        var accessor = provider.GetRequiredService<IHttpContextAccessor>();

        return enrichmentConfiguration.With(new RequestHeadersEnricher(accessor));
    }

    public static LoggerConfiguration WithConnectionDetails(this LoggerEnrichmentConfiguration enrichmentConfiguration, IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        var provider = services.BuildServiceProvider();
        var accessor = provider.GetRequiredService<IHttpContextAccessor>();

        return enrichmentConfiguration.With(new ConnectionDetailsEnricher(accessor));
    }
}
