using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using System.Globalization;
using System.Runtime.CompilerServices;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Dkw.Abp.OpenId;

public class OpenIdDataSeedContributor :
    IDataSeedContributor,
    ITransientDependency,
    IDisposable
{

    // ToDo: This should be a value obtained from IConfiguration. -dw
    private static readonly String DefaultAccessTokenLifespan = TimeSpan.FromMinutes(10).ToString("c", CultureInfo.InvariantCulture);

    private readonly IConfiguration _configuration;
    private readonly ILogger<OpenIdDataSeedContributor> _logger;
    private readonly IServiceScope _scope;
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly HashSet<String> _createdClientIds = [];

    private static readonly String[] BuiltInScopes =
    [
        Permissions.Scopes.Address,
        Permissions.Scopes.Email,
        Permissions.Scopes.Phone,
        Permissions.Scopes.Profile,
        Permissions.Scopes.Roles
    ];

    private static readonly String[] BuiltInGrantTypes =
    [
        GrantTypes.AuthorizationCode,
        GrantTypes.ClientCredentials,
        GrantTypes.DeviceCode,
        GrantTypes.Implicit,
        GrantTypes.Password,
        GrantTypes.RefreshToken,
    ];

    public OpenIdDataSeedContributor(
          IConfiguration configuration,
          ILogger<OpenIdDataSeedContributor> logger,
          IServiceProvider serviceProvider
        )
    {
        _configuration = configuration;
        _logger = logger;

        _scope = serviceProvider.CreateScope();
        _applicationManager = _scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
        _scopeManager = _scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();
    }

    public void Dispose()
    {
        _scope.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await SeedScopes();
        await SeedApplications();
    }

    private async Task SeedScopes()
    {
        _logger.LogInformation("Started Seeding OpenId Scopes ...");

        await foreach (var scope in GetScopesAsync())
        {
            await CreateScope(scope);
        }

        _logger.LogInformation("Completed Seeding OpenId Scopes.");
    }

    private async Task SeedApplications()
    {
        _logger.LogInformation("Started Seeding OpenId Applications ...");

        await foreach (var endpoint in GetEndpointsAsync())
        {
            await CreateApplicationAsync(endpoint);
        }

        _logger.LogInformation("Completed Seeding OpenId Applications.");
    }

    protected async IAsyncEnumerable<OpenIdScope> GetScopesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Delay(0, cancellationToken);
        var source = _configuration.GetSection(OpenIdDefaults.ScopesKey).Get<OpenIdScope[]>();
        if (source == null)
        {
            _logger.LogWarning("Attempting to seed the OpenID Scopes but the '{ConfigurationKey}' section was not found in any of the configuration providers.", OpenIdDefaults.ScopesKey);
            yield break;
        }

        foreach (var endpoint in source)
        {
            yield return endpoint;
        }
    }

    protected async IAsyncEnumerable<OpenIdApplication> GetEndpointsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.Delay(0, cancellationToken);
        var source = _configuration.GetSection(OpenIdDefaults.ApplicationsKey).Get<OpenIdApplication[]>();

        if (source == null)
        {
            _logger.LogWarning("Attempting to seed the OpenID Clients but the '{ConfigurationKey}' section was not found in any of the configuration providers.", OpenIdDefaults.ApplicationsKey);
            yield break;
        }

        foreach (var endpoint in source)
        {
            yield return endpoint;
        }
    }

    private async Task CreateApplicationAsync(OpenIdApplication endpoint, CancellationToken cancellationToken = default)
    {
        if (!endpoint.AllowUpdate && await _applicationManager.FindByClientIdAsync(endpoint.ClientId, cancellationToken) is not null)
        {
            _logger.LogInformation("[{Id}] The OpenID Application \"{ClientId}\" already exists in the database and does not allow updates.",
                endpoint.Key, endpoint.ClientId);
            return;
        }

        ThrowIfInValid(endpoint);

        var application = new OpenIddictApplicationDescriptor
        {
            ClientId = endpoint.ClientId,
            ClientType = endpoint.ClientType,
            ClientSecret = endpoint.ClientSecret,
            ConsentType = endpoint.ConsentType,
            DisplayName = endpoint.DisplayName,
            ApplicationType = ApplicationTypes.Web,
            Settings = {
                [Settings.TokenLifetimes.AccessToken] = DefaultAccessTokenLifespan
            }
        };

        if (endpoint.UsePKCE)
        {
            application.Requirements.Add(Requirements.Features.ProofKeyForCodeExchange);
        }

        if ((new[] { GrantTypes.AuthorizationCode, GrantTypes.Implicit }).All(endpoint.GrantTypes.Contains))
        {
            application.Permissions.Add(Permissions.ResponseTypes.CodeIdToken);

            if (String.Equals(endpoint.ClientType, ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
            {
                application.Permissions.Add(Permissions.ResponseTypes.CodeIdTokenToken);
                application.Permissions.Add(Permissions.ResponseTypes.CodeToken);
            }
        }

        if (endpoint.RedirectUris.Length > 0 || endpoint.PostLogoutRedirectUris.Length > 0)
        {
            application.Permissions.Add(Permissions.Endpoints.EndSession);
        }

        foreach (var grantType in endpoint.GrantTypes)
        {
            if (grantType == GrantTypes.AuthorizationCode)
            {
                application.Permissions.Add(Permissions.GrantTypes.AuthorizationCode);
                application.Permissions.Add(Permissions.ResponseTypes.Code);
            }

            if (grantType is GrantTypes.AuthorizationCode or
                GrantTypes.Implicit)
            {
                application.Permissions.Add(Permissions.Endpoints.Authorization);
            }

            if (grantType is GrantTypes.AuthorizationCode or
                GrantTypes.ClientCredentials or
                GrantTypes.Password or
                GrantTypes.RefreshToken or
                GrantTypes.DeviceCode)
            {
                application.Permissions.Add(Permissions.Endpoints.Token);
                application.Permissions.Add(Permissions.Endpoints.Revocation);
                application.Permissions.Add(Permissions.Endpoints.Introspection);
            }

            if (grantType == GrantTypes.ClientCredentials)
            {
                application.Permissions.Add(Permissions.GrantTypes.ClientCredentials);
            }

            if (grantType == GrantTypes.Implicit)
            {
                application.Permissions.Add(Permissions.GrantTypes.Implicit);
            }

            if (grantType == GrantTypes.Password)
            {
                application.Permissions.Add(Permissions.GrantTypes.Password);
            }

            if (grantType == GrantTypes.RefreshToken)
            {
                application.Permissions.Add(Permissions.GrantTypes.RefreshToken);
            }

            if (grantType == GrantTypes.DeviceCode)
            {
                application.Permissions.Add(Permissions.GrantTypes.DeviceCode);
                application.Permissions.Add(Permissions.Endpoints.DeviceAuthorization);
            }

            if (grantType == GrantTypes.Implicit)
            {
                application.Permissions.Add(Permissions.ResponseTypes.IdToken);
                if (String.Equals(endpoint.ClientType, ClientTypes.Public, StringComparison.OrdinalIgnoreCase))
                {
                    application.Permissions.Add(Permissions.ResponseTypes.IdTokenToken);
                    application.Permissions.Add(Permissions.ResponseTypes.Token);
                }
            }

            if (!BuiltInGrantTypes.Contains(grantType))
            {
                application.Permissions.Add(Permissions.Prefixes.GrantType + grantType);
            }
        }

        foreach (var scope in endpoint.Scopes)
        {
            if (BuiltInScopes.Contains(scope))
            {
                application.Permissions.Add(scope);
            }
            else
            {
                application.Permissions.Add(Permissions.Prefixes.Scope + scope);
            }
        }

        if (endpoint.RedirectUris.Length > 0)
        {
            foreach (var url in endpoint.RedirectUris)
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
                {
                    throw new InvalidOperationException($"ClientID {endpoint.ClientId} has an invalid RedirectUri: {url}");
                }

                if (application.RedirectUris.All(x => x != uri))
                {
                    application.RedirectUris.Add(uri);
                }
            }
        }

        if (endpoint.PostLogoutRedirectUris.Length > 0)
        {
            foreach (var url in endpoint.PostLogoutRedirectUris)
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
                {
                    throw new InvalidOperationException($"ClientID {endpoint.ClientId} has an invalid PostLogoutRedirectUri: {url}");
                }

                if (application.PostLogoutRedirectUris.All(x => x != uri))
                {
                    application.PostLogoutRedirectUris.Add(uri);
                }
            }
        }

        var client = await _applicationManager.FindByClientIdAsync(endpoint.ClientId, cancellationToken);
        if (client == null)
        {
            await _applicationManager.CreateAsync(application, cancellationToken);
            _logger.LogInformation("[{Id}] Created application: {ClientId}",
                endpoint.Key, endpoint.ClientId);
            _createdClientIds.Add(endpoint.ClientId);
            return;
        }
        else if (endpoint.AllowUpdate)
        {
            await _applicationManager.UpdateAsync(client, application, cancellationToken);
            _logger.LogInformation("[{Id}] Updated application: {ClientId}",
                endpoint.Key, endpoint.ClientId);
            _createdClientIds.Add(endpoint.ClientId);
            return;
        }
    }

    private void ThrowIfInValid(OpenIdApplication endpoint)
    {
        if (_createdClientIds.Contains(endpoint.ClientId))
        {
            throw new InvalidOperationException($"[{endpoint.Key}] ClientID {endpoint.ClientId} has already been created. Endpoint: {endpoint.Key}");
        }

        if (String.Equals(endpoint.ClientType, ClientTypes.Public, StringComparison.OrdinalIgnoreCase) && !String.IsNullOrEmpty(endpoint.ClientSecret))
        {
            throw new InvalidOperationException($"[{endpoint.Key}] Do not set a Client Secret for a Public application. Endpoint: {endpoint.Key}");
        }

        if (String.Equals(endpoint.ClientType, ClientTypes.Confidential, StringComparison.OrdinalIgnoreCase) && String.IsNullOrEmpty(endpoint.ClientSecret))
        {
            throw new InvalidOperationException($"[{endpoint.Key}] A Client Secret is required for a Confidential application. Endpoint: {endpoint.Key}");
        }

        if (endpoint.GrantTypes is null || endpoint.GrantTypes.Length == 0)
        {
            throw new InvalidOperationException($"[{endpoint.Key}] At least one GrantType must be specified. Endpoint: {endpoint.Key}");
        }

        if (endpoint.ClientType == ClientTypes.Public && (endpoint.Scopes is null || endpoint.Scopes.Length == 0))
        {
            throw new InvalidOperationException($"[{endpoint.Key}] Scopes must be specified for a Public client. Endpoint: {endpoint.Key}");
        }

        foreach (var url in endpoint.RedirectUris)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
            {
                throw new InvalidOperationException($"[{endpoint.Key}] ClientID {endpoint.ClientId} has an invalid RedirectUri: {url}");
            }
        }

        foreach (var url in endpoint.PostLogoutRedirectUris)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
            {
                throw new InvalidOperationException($"[{endpoint.Key}] ClientID {endpoint.ClientId} has an invalid PostLogoutRedirectUri: {url}");
            }
        }
    }

    private async Task CreateScope(OpenIdScope scope, CancellationToken cancellationToken = default)
    {
        var descriptor = new OpenIddictScopeDescriptor
        {
            Name = scope.Name,
            DisplayName = scope.DisplayName
        };

        descriptor.Resources.Add(scope.Name);
        foreach (var resource in scope.Resources)
        {
            descriptor.Resources.Add(resource);
        }

        var existing = await _scopeManager.FindByNameAsync(scope.Name, cancellationToken);
        if (existing == null)
        {
            await _scopeManager.CreateAsync(descriptor, cancellationToken);
            _logger.LogInformation("[{ScopeId}] Created scope: {Scope}",
                scope.Name, scope.DisplayName);
        }
        else
        {
            await _scopeManager.UpdateAsync(existing, descriptor, cancellationToken);
            _logger.LogInformation("[{ScopeId}] Updated scope: {Scope}",
                scope.Name, scope.DisplayName);
        }
    }
}
