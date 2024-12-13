using DKW.Abp.EntityFrameworkCore.Migrations;

namespace TestApp.EntityFrameworkCore.Sqlite;

public class TestAppDbContextMigrator(IServiceProvider serviceProvider)
    : DbContextMigrator<TestAppDbContext>(serviceProvider);
