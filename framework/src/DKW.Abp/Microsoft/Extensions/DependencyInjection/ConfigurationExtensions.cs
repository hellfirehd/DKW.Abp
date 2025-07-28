using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigurationExtensions
{
    public static T GetOptions<T>(this IConfiguration configuration)
            where T : class, new()
            => configuration.GetOptionsOrNull<T>() ?? new T();

    public static T? GetOptionsOrNull<T>(this IConfiguration configuration)
            where T : class
            => configuration.GetSection(typeof(T).Name).Get<T>();
}
