using DKW.Abp.OpenId;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace Microsoft.Extensions.DependencyInjection;

public static class OpenIdServiceCollectionExtensions
{
    private const String NotRegistered = $"{nameof(GetOpenIdEndpoints)} was called but the '{nameof(IOpenIdEndpointProvider)}' has not been registered for dependency injection. Did you remember to call {nameof(AddDkwOpenIdEndpoints)}() or {nameof(AddDkwOpenIdEndpoints)}()?";

    public static IHostBuilder AddOpenIdEndpoints(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((ctx, configuration) =>
        {
            var env = ctx.HostingEnvironment.EnvironmentName;
            configuration.AddJsonFile(String.Format(OpenIdDefaults.AppSettingsFilename, env), optional: false);
        });

        builder.ConfigureServices((ctx, services) =>
        {
            var provider = new OpenIdEndpointProvider(ctx.Configuration);
            services.TryAddSingleton<IOpenIdEndpointProvider>(provider);
        });

        return builder;
    }

    public static IHostApplicationBuilder AddOpenIdEndpoints(this IHostApplicationBuilder builder)
    {
        var env = builder.Environment.EnvironmentName;
        builder.Configuration.AddJsonFile(String.Format(OpenIdDefaults.AppSettingsFilename, env), optional: false);

        var provider = new OpenIdEndpointProvider(builder.Configuration);
        builder.Services.TryAddSingleton<IOpenIdEndpointProvider>(provider);

        return builder;
    }

    public static IHostBuilder AddDkwOpenIdEndpoints(this IHostBuilder builder)
    {
        builder.AddOpenIdEndpoints();

        builder.ConfigureServices((_, services) =>
        {
            //
            services.TryAddSingleton(new DkwOpenIdEndpoints(services.GetOpenIdEndpoints()));
        });

        return builder;
    }

    public static IHostApplicationBuilder AddDkwOpenIdEndpoints(this IHostApplicationBuilder builder)
    {
        builder.AddOpenIdEndpoints();

        builder.Services.TryAddSingleton(new DkwOpenIdEndpoints(builder.Services.GetOpenIdEndpoints()));

        return builder;
    }

    public static IOpenIdEndpointProvider GetOpenIdEndpoints(this IServiceCollection services)
        => services.GetSingletonInstanceOrNull<IOpenIdEndpointProvider>()
            ?? throw new InvalidOperationException(NotRegistered);

    public static IOpenIdEndpointProvider GetOpenIdEndpoints(this IServiceProvider serviceProvider)
        => serviceProvider.GetService<IOpenIdEndpointProvider>()
            ?? throw new InvalidOperationException(NotRegistered);

    public static DkwOpenIdEndpoints GetDkwEndpoints(this IConfiguration configuration)
        => new(new OpenIdEndpointProvider(configuration));

    public static DkwOpenIdEndpoints GetDkwEndpoints(this IServiceCollection services)
    {
        var it = services.FirstOrDefault(d => d.ServiceType == typeof(IOpenIdEndpointProvider));
        var other = it?.NormalizedImplementationInstance();
        var provider = services.GetSingletonInstanceOrNull<IOpenIdEndpointProvider>()
                ?? throw new InvalidOperationException(NotRegistered);
        return new DkwOpenIdEndpoints(provider);
    }

    public static DkwOpenIdEndpoints GetDkwEndpoints(this IServiceProvider serviceProvider)
        => serviceProvider.GetService<DkwOpenIdEndpoints>()
            ?? throw new InvalidOperationException(NotRegistered);

    public static DkwOpenIdEndpoints GetDkwEndpoints(this ApplicationInitializationContext context)
        => context.ServiceProvider.GetService<DkwOpenIdEndpoints>()
            ?? throw new InvalidOperationException(NotRegistered);
}
