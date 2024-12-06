using Flurl;

namespace Inara.Abp.OpenId;

public class InaraOpenIdEndpoints(IOpenIdEndpointProvider provider) : IOpenIdAuthority
{
    private const String IdentityProviderKey = "InaraAuthority";

    private readonly IOpenIdEndpointProvider _provider = provider;
    public OpenIdEndpoint this[String key] => _provider[key];

    public OpenIdEndpoint Authority => _provider[IdentityProviderKey];

    public String AuthorityUrl => Authority.BaseUrl;
    public Uri AuthorityUri => new(AuthorityUrl, UriKind.Absolute);

    public String MetadataUrl
        => AuthorityUrl.AppendPathSegment(".well-known/openid-configuration");

    public Uri MetadataUri => new(MetadataUrl, UriKind.Absolute);
}