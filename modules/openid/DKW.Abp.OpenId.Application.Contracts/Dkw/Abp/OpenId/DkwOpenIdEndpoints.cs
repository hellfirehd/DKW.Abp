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

using Flurl;

namespace Dkw.Abp.OpenId;

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
