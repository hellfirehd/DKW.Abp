using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Volo.Abp.EntityFrameworkCore;

namespace DKW.Abp.EntityFrameworkCore;

public class OpenIdDbContextFactory : IDesignTimeDbContextFactory<OpenIdDbContext>
{
    public OpenIdDbContext CreateDbContext(String[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var builder = new DbContextOptionsBuilder<OpenIdDbContext>()
            .UseNpgsql(options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

        return new OpenIdDbContext(builder.Options);
    }
}