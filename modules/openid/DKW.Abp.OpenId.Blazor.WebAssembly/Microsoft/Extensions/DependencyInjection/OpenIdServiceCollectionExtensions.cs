using DKW.Abp.OpenId;
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
