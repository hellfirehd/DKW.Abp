using Dkw.Abp.EntityFrameworkCore.Seeding;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class SeedingExtensions
{
    public static async Task SeedAsync(this IHost host)
    {
        ArgumentNullException.ThrowIfNull(host);

        await host.Services.SeedAsync();
    }

    public static async Task SeedAsync(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            await scope.SeedAsync();
        }
    }

    public static async Task SeedAsync(this IServiceScope scope)
    {
        ArgumentNullException.ThrowIfNull(scope);

        var dataSeeder = scope.ServiceProvider.GetRequiredService<SeedingManager>();
        await dataSeeder.SeedAsync();
    }
}
