namespace Dkw.Abp.OpenId;

public interface IOpenIdAuthority
{
    String AuthorityUrl { get; }
    Uri AuthorityUri { get; }
    OpenIdEndpoint Authority { get; }
    String MetadataUrl { get; }
    Uri MetadataUri { get; }
}
