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

using Dkw.Abp;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static String[] GetCorsAllowedOrigins(this IServiceCollection services)
      => services.GetConfiguration().GetCorsAllowedOrigins();

    public static String[] GetCorsAllowedOrigins(this IConfiguration configuration)
    {
        var r = configuration
            .GetSection(DkwKeys.Configuration.CorsAllowedOrigins)
            .Get<String[]>()
            ?? [];

        return [.. r.Select(o => o.TrimEnd('/'))];
    }

    public static IServiceCollection AddLoggedModule<T>(this IServiceCollection services)
        where T : class, ILoggedModule<T>, new()
    {
        var module = new T();
        var name = module.GetType().Assembly.GetName().Name!;

        module.SetProperties(name, ThisAssembly.AssemblyVersion);

        services.AddSingleton<ILoggedModule>(module);

        services.AddObjectAccessor<ILoggedModule>(module);

        return services;
    }
}
