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

using System.Net;

namespace Dkw.Abp.OpenId.WebAssembly.Authorization;

// Original source: https://github.com/berhir/BlazorWebAssemblyCookieAuth.
public class AuthorizedHandler(HostAuthenticationStateProvider authenticationStateProvider) : DelegatingHandler
{
    private readonly HostAuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        HttpResponseMessage responseMessage;
        if (authState.User.Identity?.IsAuthenticated == false)
        {
            // if user is not authenticated, immediately set response status to 401 Unauthorized
            responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
        else
        {
            responseMessage = await base.SendAsync(request, cancellationToken);
        }

        if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
        {
            // if server returned 401 Unauthorized, redirect to login page
            _authenticationStateProvider.SignIn();
        }

        return responseMessage;
    }
}
