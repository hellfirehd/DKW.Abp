using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestApp.EntityFrameworkCore.Sqlite;

public class TestAppDbContextFactory : IDesignTimeDbContextFactory<TestAppDbContext>
{
    public TestAppDbContext CreateDbContext(String[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var builder = new DbContextOptionsBuilder<TestAppDbContext>()
            .UseSqlite(options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

        return new TestAppDbContext(builder.Options);
    }
}
