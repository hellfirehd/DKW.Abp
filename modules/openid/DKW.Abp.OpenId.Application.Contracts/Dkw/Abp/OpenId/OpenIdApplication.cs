using System.Diagnostics;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace DKW.Abp.OpenId;

[DebuggerDisplay("{Key}:{ClientId}")]
public class OpenIdApplication : OpenIdEndpoint
{
    public Boolean AllowUpdate { get; set; } = true;

    public String ApplicationType { get; set; } = ApplicationTypes.Web;

    public String ClientType { get; set; } = ClientTypes.Public;

    public String ConsentType { get; set; } = ConsentTypes.Explicit;

    public String[] Permissions { get; set; } = [];

    public String[] PostLogoutRedirectUris { get; set; } = [];

    public Dictionary<String, String> Properties { get; set; } = [];

    public String[] Requirements { get; set; } = [];

    public Dictionary<String, String> Settings { get; set; } = [];
}
