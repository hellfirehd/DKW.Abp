using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TestApp.EntityFrameworkCore.Sqlite;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Xunit;

namespace TestApp.EntityFrameworkCore;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(TestAppTestConsts.CollectionDefinitionName)]
public class Migration_and_Seeding_test
{
    [Fact]
    public async Task Should_Apply_Migrations_and_Seed()
    {
        using (var application = await AbpApplicationFactory.CreateAsync<MigrationsTestModule>(options =>
        {
            options.UseAutofac();
        }))
        {
            await application.InitializeAsync();

            await application.ServiceProvider.MigrateAsync();

            await application.ServiceProvider.SeedAsync();

            var dbContext = application.ServiceProvider.GetRequiredService<TestAppDbContext>();

            dbContext.People.Count().ShouldBe(1);

            await application.ShutdownAsync();
        }
    }

    [DependsOn(typeof(TestAppApplicationTestModule))]
    [DependsOn(typeof(TestAppEntityFrameworkCoreSqliteModule))]
    public class MigrationsTestModule : AbpModule
    {
        private SqliteConnection? _sqliteConnection;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysDisableUnitOfWorkTransaction();

            ConfigureInMemorySqlite(context.Services);
        }

        private void ConfigureInMemorySqlite(IServiceCollection services)
        {
            _sqliteConnection = CreateDatabaseAndGetConnection();

            services.Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(context =>
                {
                    context.DbContextOptions.UseSqlite(_sqliteConnection);
                });
            });
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            _sqliteConnection?.Dispose();
        }

        protected virtual SqliteConnection CreateDatabaseAndGetConnection()
        {
            //var connection = new AbpUnitTestSqliteConnection("Data Source=:memory:");
            var connection = new AbpUnitTestSqliteConnection($"Data Source=E:\\Test-{Guid.Empty:N}.db");
            connection.Open();

            var options = new DbContextOptionsBuilder<TestAppDbContext>()
                .UseSqlite(connection, sql => sql.MigrationsAssembly(typeof(TestAppEntityFrameworkCoreSqliteModule).Assembly))
                .Options;

            using (var context = new TestAppDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().Create();
            }

            return connection;
        }
    }
}
