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

using Dkw.Abp.OpenId;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class OpenIdServiceCollectionExtensions
{
    public static async Task<WebAssemblyHostBuilder> AddOpenIdEndpointsAsync(this WebAssemblyHostBuilder builder)
    {
        using (var http = new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
        {
            var path = String.Format(OpenIdDefaults.AppSettingsFilename, builder.HostEnvironment.Environment);
            await http.GetAsync(path).ContinueWith(async response =>
            {
                var json = await response.Result.Content.ReadAsStringAsync();
                builder.Configuration.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));
            });
        }

        var provider = new OpenIdEndpointProvider(builder.Configuration);
        builder.Services.TryAddSingleton<IOpenIdEndpointProvider>(provider);

        return builder;
    }

    public static async Task<WebAssemblyHostBuilder> AddDkwOpenIdEndpointsAsync(this WebAssemblyHostBuilder builder)
    {
        await builder.AddOpenIdEndpointsAsync();

        builder.Services.TryAddSingleton(new DkwOpenIdEndpoints(builder.Services.GetOpenIdEndpoints()));

        return builder;
    }
}
