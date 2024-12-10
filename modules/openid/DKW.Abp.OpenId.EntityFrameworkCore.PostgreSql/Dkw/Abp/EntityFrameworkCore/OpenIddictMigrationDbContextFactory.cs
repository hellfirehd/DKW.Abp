using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Volo.Abp.EntityFrameworkCore;

namespace DKW.Abp.EntityFrameworkCore;

public class OpenIddictMigrationDbContextFactory : IDesignTimeDbContextFactory<OpenIddictMigrationDbContext>
{
    private const String ConnectionString = "Server=postgresql;Port=5432;Database=OpenIddict;User Id=postgres;Password=1q2w3E*";

    public OpenIddictMigrationDbContext CreateDbContext(String[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var name = typeof(OpenIddictMigrationDbContext).Assembly.GetName().Name;

        var builder = new DbContextOptionsBuilder<OpenIddictMigrationDbContext>()
            .UseNpgsql(ConnectionString, options => options.MigrationsAssembly(name));

        return new OpenIddictMigrationDbContext(builder.Options);
    }
}