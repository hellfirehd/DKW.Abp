using Flurl;

namespace DKW.Abp.OpenId;

public class DkwOpenIdEndpoints(IOpenIdEndpointProvider provider) : IOpenIdAuthority
{
    private const String IdentityProviderKey = "DkwAuthority";

    private readonly IOpenIdEndpointProvider _provider = provider;
    public OpenIdEndpoint this[String key] => _provider[key];

    public OpenIdEndpoint Authority => _provider[IdentityProviderKey];

    public String AuthorityUrl => Authority.BaseUrl;
    public Uri AuthorityUri => new(AuthorityUrl, UriKind.Absolute);

    public String MetadataUrl
        => AuthorityUrl.AppendPathSegment(".well-known/openid-configuration");

    public Uri MetadataUri => new(MetadataUrl, UriKind.Absolute);
}