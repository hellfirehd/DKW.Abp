using DKW.Abp.EntityFrameworkCore.Migrations;

namespace DKW.Abp.EntityFrameworkCore;

public class OpenIddictMigrationDbContextMigrator(IServiceProvider serviceProvider)
    : DbContextMigrator<OpenIddictMigrationDbContext>(serviceProvider);