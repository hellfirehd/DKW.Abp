using Dkw.Abp;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Modularity;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static String[] GetCorsAllowedOrigins(this IServiceCollection services)
      => services.GetConfiguration().GetCorsAllowedOrigins();

    public static String[] GetCorsAllowedOrigins(this IConfiguration configuration)
    {
        var r = configuration
            .GetSection(DkwKeys.Configuration.CorsAllowedOrigins)
            .Get<String[]>()
            ?? [];

        return [.. r.Select(o => o.TrimEnd('/'))];
    }

    public static IServiceCollection AddLoggedModule<T>(this IServiceCollection services)
        where T : class, ILoggedModule<T>, new()
    {
        var module = new T();
        var name = module.GetType().Assembly.GetName().Name!;

        module.SetProperties(name, ThisAssembly.AssemblyVersion);

        services.AddSingleton<ILoggedModule>(module);

        services.AddObjectAccessor<ILoggedModule>(module);

        return services;
    }
}
