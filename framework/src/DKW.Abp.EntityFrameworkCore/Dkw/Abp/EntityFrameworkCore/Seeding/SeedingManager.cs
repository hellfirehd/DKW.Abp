using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Dkw.Abp.EntityFrameworkCore.Seeding;

public class SeedingManager(
    ILogger<SeedingManager> logger,
    IServiceScopeFactory serviceScopeFactory
    ) : ITransientDependency
{
    private readonly ILogger<SeedingManager> _logger = logger;

    public IServiceScopeFactory ServiceScopeFactory { get; } = serviceScopeFactory;

    public async Task SeedAsync()
    {
        _logger.LogInformation("Seeding data...");

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
            await dataSeeder.SeedAsync();
        }

        _logger.LogInformation("Completed seeding data.");
    }
}
