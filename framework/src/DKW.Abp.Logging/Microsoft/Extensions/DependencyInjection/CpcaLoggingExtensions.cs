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

using Dkw.Abp.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;

namespace Microsoft.Extensions.DependencyInjection;

public static class DkwLoggingExtensions
{

    public static IHostBuilder UseCpcaSerilog<TModule>(this IHostBuilder builder, Action<LoggerConfiguration>? configure = null)
        where TModule : class, ILoggedModule<TModule>, new()
    {
        builder.ConfigureServices((ctx, services) =>
        {
            services.AddLoggedModule<TModule>();
            services.AddCpcaSerilog(configure);
        });

        return builder;
    }

    public static IServiceCollection AddCpcaSerilog<TModule>(this IServiceCollection services, Action<LoggerConfiguration>? configure = null)
        where TModule : class, ILoggedModule<TModule>, new()
    {
        services.AddLoggedModule<TModule>();

        return services.AddCpcaSerilog(configure);
    }

    /// <summary>
    /// Configures and adds CPCA logging conventions to the <see cref="IServiceCollection"/>.
    /// </summary>
    public static IServiceCollection AddCpcaSerilog(this IServiceCollection services, Action<LoggerConfiguration>? configure = null)
    {
        var configuration = services.GetConfiguration();

        var seq = configuration.GetOptionsOrNull<Seq>();

        var loggerConfig = DkwLogging.DefaultConfiguration
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithCurrentPrincipal(services.GetRequiredService<ICurrentPrincipalAccessor>())
            .Enrich.WithEnvironmentName()
            .AddSeq(seq);

        if (services.GetObject<ILoggedModule>() is ILoggedModule module)
        {
            loggerConfig.Enrich.WithLoggedModule(module);
        }

        configure?.Invoke(loggerConfig);

        var logger = loggerConfig.CreateLogger();

        logger.Information("Initialized logging: {Environment}", GetEnvironment());
        logger.Debug("Debug logging is enabled.");
        logger.Verbose("Verbose logging is enabled.");

        return services.AddSerilog(logger, dispose: false);
    }

    private static String GetEnvironment()
        => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
        ?? "Production";

    private static LoggerConfiguration AddSeq(this LoggerConfiguration loggerConfig, Seq? seq)
    {
        if (seq is not null && seq.Enabled)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(seq.ServerUrl) && !String.IsNullOrWhiteSpace(seq.ApiKey))
                {
                    return loggerConfig.WriteTo.Seq(serverUrl: seq.ServerUrl, apiKey: seq.ApiKey, formatProvider: CultureInfo.InvariantCulture);
                }
                else if (!String.IsNullOrWhiteSpace(seq.ServerUrl))
                {
                    return loggerConfig.WriteTo.Seq(serverUrl: seq.ServerUrl, formatProvider: CultureInfo.InvariantCulture);
                }
            }
            finally
            {
                Log.Information("Seq logging is enabled!  ServerUrl: {ServerUrl}", seq.ServerUrl);
            }
        }

        return loggerConfig;
    }
}
