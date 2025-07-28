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
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class SelfLogServiceCollectionExtensions
{
    public static IServiceCollection AddSelfLog(this IServiceCollection services, Action<SelfLogOptions>? configure = null)
    {
        services.AddSingleton<SelfLog>();

        var configuration = services.GetConfiguration();
        var options = configuration.GetSection(SelfLogOptions.SectionName).Get<SelfLogOptions>() ?? new();

        configure?.Invoke(options);

        var selfLog = services.GetRequiredService<SelfLog>();

        if (selfLog.IsEnabled)
        {
            Serilog.Debugging.SelfLog.Enable(selfLog.LogMessage);
        }
        else
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);
        }

        return services;
    }
}
