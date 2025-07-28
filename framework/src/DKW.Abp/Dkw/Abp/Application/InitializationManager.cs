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

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Dkw.Abp.Application;

public sealed class InitializationManager(IServiceProvider serviceProvider) : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IApplicationInitializer>>();
            var services = scope.ServiceProvider.GetServices<IApplicationInitializer>();

            foreach (var initializer in services.OrderBy(s => s.Priority))
            {
                try
                {
                    if (!initializer.IsEnabled)
                    {
                        logger.LogInformation("Skipping Initialization: {Initializer} is disabled.", initializer.GetType().Name);
                        continue;
                    }

                    logger.LogDebug("Beginning Initialization: {Initializer}", initializer.GetType().Name);
                    await initializer.InitializeAsync(cancellationToken);
                    logger.LogInformation("[{EventId}] Finished Initialization: {Initializer}", InitializationEventId.InitializerCompleted, initializer.GetType().Name);
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "Initialization Failed: {Initializer}", initializer.GetType().Name);
                    if (initializer.AbortApplicationOnException)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
