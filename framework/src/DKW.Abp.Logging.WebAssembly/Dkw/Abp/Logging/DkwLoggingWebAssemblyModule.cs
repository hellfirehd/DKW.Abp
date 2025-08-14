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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Volo.Abp;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;

namespace Dkw.Abp.Logging;

[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
public class DkwLoggingWebAssemblyModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddObjectAccessor<NavigationManager>();
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.WithCurrentLocation(context.ServiceProvider.GetRequiredService<IObjectAccessor<NavigationManager>>())
            .WriteTo.BrowserConsole(
                restrictedToMinimumLevel: LogEventLevel.Information,
                outputTemplate: LoggingTemplates.BrowserConsole,
                CultureInfo.InvariantCulture,
                jsRuntime: context.ServiceProvider.GetRequiredService<IJSRuntime>())
            .CreateLogger();
    }
}
