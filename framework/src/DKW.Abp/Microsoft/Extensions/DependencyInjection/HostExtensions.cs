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

using Dkw.Abp.Application;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class HostExtensions
{
    /// <summary>
    /// Executes all the <see cref="IApplicationInitializer"/> instances found in <see cref="IApplicationBuilder.ApplicationServices"/>.
    /// </summary>
    /// <param name="host"></param>
    /// <returns></returns>
    public static async Task ExecuteApplicationInitializersAsync(this IHost host)
    {
        ArgumentNullException.ThrowIfNull(host);

        using (var scope = host.Services.CreateScope())
        {
            var manager = scope.ServiceProvider.GetRequiredService<InitializationManager>();
            await manager.InitializeAsync();
        }
    }
}
