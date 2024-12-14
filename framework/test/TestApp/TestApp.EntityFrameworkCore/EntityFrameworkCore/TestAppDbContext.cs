using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace TestApp.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class TestAppDbContext(DbContextOptions<TestAppDbContext> options) : AbpDbContext<TestAppDbContext>(options)
{
    public DbSet<Person> People { get; set; }
    public DbSet<PersonView> PersonView { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ConfigureAuditLogging();
        //modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureIdentity();
        //modelBuilder.ConfigureOpenIddict();
        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        //modelBuilder.ConfigureTenantManagement();

        modelBuilder.Entity<Phone>(b =>
        {
            b.HasKey(p => new { p.PersonId, p.Number });
        });

        modelBuilder.Entity<Person>(b =>
        {
            b.Property(x => x.LastActiveTime).ValueGeneratedOnAddOrUpdate().HasDefaultValue(DateTime.Now);
            b.Property(x => x.HasDefaultValue).HasDefaultValue(DateTime.Now);
            b.Property(x => x.TenantId).HasColumnName("Tenant_Id");
            b.Property(x => x.IsDeleted).HasColumnName("Is_Deleted");
        });

        modelBuilder.Entity<PersonView>(p =>
        {
            p.HasNoKey();
            p.ToView("View_PersonView");

            p.ApplyObjectExtensionMappings();
        });

        modelBuilder.TryConfigureObjectExtensions<TestAppDbContext>();
    }
}
