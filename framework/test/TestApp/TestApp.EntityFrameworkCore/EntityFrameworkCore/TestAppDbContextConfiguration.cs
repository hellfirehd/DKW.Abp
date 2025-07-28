using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace TestApp.EntityFrameworkCore;

public static class TestAppDbContextConfiguration
{
    public static void ConfigureTestApp(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Phone>(b =>
        {
            b.HasKey(p => new { p.PersonId, p.Number });
        });

        modelBuilder.Entity<Person>(b =>
        {
            b.Property(x => x.LastActiveTime).ValueGeneratedOnAddOrUpdate().HasDefaultValue(DateTime.UnixEpoch);
            b.Property(x => x.HasDefaultValue).HasDefaultValue(DateTime.UnixEpoch);
            b.Property(x => x.TenantId).HasColumnName("Tenant_Id");
            b.Property(x => x.IsDeleted).HasColumnName("Is_Deleted");
        });

        modelBuilder.Entity<PersonView>(p =>
        {
            p.HasNoKey();
            p.ToView("View_PersonView");

            p.ApplyObjectExtensionMappings();
        });
    }
}
