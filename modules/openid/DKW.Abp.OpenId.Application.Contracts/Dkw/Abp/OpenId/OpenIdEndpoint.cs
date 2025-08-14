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

using OpenIddict.Abstractions;
using System.Diagnostics;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Dkw.Abp.OpenId;

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
