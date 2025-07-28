using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dkw.Abp.OpenId;

public class OpenIdEndpointProvider : Dictionary<String, OpenIdEndpoint>,
    IOpenIdEndpointProvider
{
    private const String NotFound = $"{nameof(OpenIdServiceCollectionExtensions.AddDkwOpenIdEndpoints)} was called but the '{OpenIdDefaults.ApplicationsKey}' section was not found in any of the configuration providers.";

    public OpenIdEndpointProvider(IConfiguration configuration)
    {
        var endpoints = configuration.GetSection(OpenIdDefaults.ApplicationsKey).Get<OpenIdEndpoint[]>()
            ?? throw new InvalidOperationException(NotFound);

        foreach (var endpoint in endpoints)
        {
            Add(endpoint.Key, endpoint);
        }
    }

    IEnumerator<OpenIdEndpoint> IEnumerable<OpenIdEndpoint>.GetEnumerator() => Values.GetEnumerator();
}
