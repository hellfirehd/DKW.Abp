using OpenIddict.Abstractions;
using System.Diagnostics;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Inara.Abp.OpenId;

[DebuggerDisplay("{Key}:{ClientId}")]
public class OpenIdEndpoint
{
    private String? _displayName;

    public required String Key { get; set; }

    public String[] Audiences { get; set; } = [];
    public String Audience => Audiences.FirstOrDefault() ?? ClientId;

    public required String BaseUrl { get; set; }
    public Uri BaseUri => _baseUri ??= new Uri(BaseUrl, UriKind.Absolute);
    private Uri? _baseUri;

    public required String ClientId { get; set; }

    public String? ClientSecret { get; set; }

    public String DisplayName
    {
        get => _displayName ?? ClientId;
        set => _displayName = value;
    }

    public String[] GrantTypes { get; set; } = [OpenIddictConstants.GrantTypes.AuthorizationCode];

    public String ResponseMode { get; set; } = ResponseModes.Query;
    public String ResponseType { get; set; } = ResponseTypes.Code;

    [Obsolete("Use UsePKCE instead.")]
    public Boolean ProofKeyForCodeExchange { get => UsePKCE; set => UsePKCE = value; }
    public Boolean UsePKCE { get; set; } = true;
    public Boolean RequireHttpsMetadata { get; set; } = true;

    public String[] Scopes { get; set; } = [];

    public String[] RedirectUris { get; set; } = [];
}