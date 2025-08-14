// DKW ABP Framework Extensions
// Copyright (C) 2025 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

using Dkw.Abp.Microservices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        var builder = WebApplication.CreateBuilder([]);

        builder.Host.UseAutofac();

        var abp = await builder.AddApplicationAsync<MigrationsTestModule>();

        await using (var app = builder.Build())
        {
            await app.InitializeApplicationAsync();

            await app.MigrateAsync();
            await app.SeedAsync();

            var unitOfWork = app.Services.GetRequiredService<TestAppDbContext>();
            (await unitOfWork.People.CountAsync()).ShouldBe(1);
        }
    }

    [DependsOn(typeof(TestAppApplicationModule))] // This references TestAppDomainModule which contains the PersonContributor.
    [DependsOn(typeof(TestAppEntityFrameworkCoreSqliteModule))] // This contains the migrations.
    [DependsOn(typeof(DkwMicroserviceModule))] // This references things that ABP needs to run.
    public class MigrationsTestModule : AbpModule
    {
        // Holds the connection open for the duration of the test
        private SqliteConnection? _sqliteConnection;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysDisableUnitOfWorkTransaction();

            _sqliteConnection = GetOpenConnection();

            context.Services.Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(context =>
                {
                    context.DbContextOptions.UseSqlite(_sqliteConnection, conf =>
                    {
                        conf.MigrationsAssembly(typeof(TestAppEntityFrameworkCoreSqliteModule).Assembly.GetName().Name);
                    });
                });
            });
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            _sqliteConnection?.Dispose();
        }

        private static AbpUnitTestSqliteConnection GetOpenConnection()
        {
            var connection = new AbpUnitTestSqliteConnection("Data Source=file::memory:?cache=shared");
            connection.Open();
            return connection;
        }
    }
}
