using Inara.Abp.EntityFrameworkCore.Migrations;

namespace Inara.Abp.EntityFrameworkCore;

public class OpenIddictMigrationDbContextMigrator(IServiceProvider serviceProvider)
    : DbContextMigrator<OpenIddictMigrationDbContext>(serviceProvider);