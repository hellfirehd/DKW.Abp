using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Dkw.Abp.OpenId.WebAssembly.Authorization;

// Original source: https://github.com/berhir/BlazorWebAssemblyCookieAuth.
public class HostAuthenticationStateProvider(
    NavigationManager navigation,
    HttpClient client,
    ILogger<HostAuthenticationStateProvider> logger
    ) : AuthenticationStateProvider
{
    private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromSeconds(60);

    private const String LogInPath = "api/Account/Login";
    //private const String LogOutPath = "api/Account/Logout";

    private readonly NavigationManager _navigation = navigation;
    private readonly HttpClient _client = client;
    private readonly ILogger<HostAuthenticationStateProvider> _logger = logger;

    private DateTimeOffset _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
    private ClaimsPrincipal _cachedUser = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(await GetUser(useCache: true));
    }

    public void SignIn(String? customReturnUrl = null)
    {
        var returnUrl = customReturnUrl != null ? _navigation.ToAbsoluteUri(customReturnUrl).ToString() : null;
        var encodedReturnUrl = Uri.EscapeDataString(returnUrl ?? _navigation.Uri);
        var logInUrl = _navigation.ToAbsoluteUri($"{LogInPath}?returnUrl={encodedReturnUrl}");
        _navigation.NavigateTo(logInUrl.ToString(), true);
    }

    private async ValueTask<ClaimsPrincipal> GetUser(Boolean useCache = false)
    {
        var now = DateTimeOffset.Now;
        if (useCache && now < _userLastCheck + UserCacheRefreshInterval)
        {
            _logger.LogDebug("Taking user from cache");
            return _cachedUser;
        }

        _logger.LogDebug("Fetching user");
        _cachedUser = await FetchUser();
        _userLastCheck = now;

        return _cachedUser;
    }

    private async Task<ClaimsPrincipal> FetchUser()
    {
        UserDto? user = null;

        try
        {
            _logger.LogInformation("BaseAddress: {BaseAddress}", _client.BaseAddress?.ToString());
            user = await _client.GetFromJsonAsync<UserDto>("api/User");
        }
        catch (Exception exc)
        {
            _logger.LogWarning(exc, "Fetching user failed.");
        }

        if (user?.IsAuthenticated != true)
        {
            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        var identity = new ClaimsIdentity(
            nameof(HostAuthenticationStateProvider),
            user.NameClaimType,
            user.RoleClaimType);

        if (user.Claims != null)
        {
            foreach (var claim in user.Claims)
            {
                identity.AddClaim(new Claim(claim.Type, claim.Value));
            }
        }

        return new ClaimsPrincipal(identity);
    }
}
