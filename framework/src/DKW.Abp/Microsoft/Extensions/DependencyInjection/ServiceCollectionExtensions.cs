using DKW.Abp;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static String[] GetCorsAllowedOrigins(this IServiceCollection services)
      => services.GetConfiguration().GetCorsAllowedOrigins();

    public static String[] GetCorsAllowedOrigins(this IConfiguration configuration)
    {
        var r = configuration
            .GetSection(DkwAbpKeys.Configuration.CorsAllowedOrigins)
            .Get<String[]>()
            ?? [];

        return r.Select(o => o.TrimEnd('/')).ToArray();
    }
}