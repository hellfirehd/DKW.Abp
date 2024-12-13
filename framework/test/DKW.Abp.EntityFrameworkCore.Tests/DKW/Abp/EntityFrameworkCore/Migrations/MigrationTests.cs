using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestApp;
using TestApp.EntityFrameworkCore.Sqlite;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace DKW.Abp.EntityFrameworkCore.Migrations;

public class MigrationTests
{
    [Fact]
    public async Task ApplyMigrations()
    {
        var configuration = new ConfigurationBuilder().Build();

        using (var application = await AbpApplicationFactory.CreateAsync<TestAppMigrationModule>(options =>
        {
            options.Services.ReplaceConfiguration(configuration);
            options.UseAutofac();
        }))
        {
            await application.InitializeAsync();

            var migrator = application.ServiceProvider.GetRequiredService<TestAppDbContextMigrator>();

            await application.ServiceProvider.MigrateAsync();

            await application.ShutdownAsync();
        }
    }

    [DependsOn(typeof(TestAppApplicationModule))]
    [DependsOn(typeof(TestAppEntityFrameworkCoreSqliteModule))]
    public class TestAppMigrationModule : AbpModule
    {
    }
}
