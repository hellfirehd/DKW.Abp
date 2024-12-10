using System.Diagnostics;

namespace DKW.Abp.OpenId;

[DebuggerDisplay("{Key}:{ClientId}")]
public class OpenIdScope
{
    private String? _displayName;

    public required String Key { get; set; }
    public required String Name { get; set; }
    public String DisplayName
    {
        get => _displayName ?? Name;
        set => _displayName = value;
    }
    public String[] Resources { get; set; } = [];
}