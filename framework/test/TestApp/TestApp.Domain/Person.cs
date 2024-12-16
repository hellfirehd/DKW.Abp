using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace TestApp;

public class Person : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
{
    public virtual Guid? TenantId { get; set; }

    public virtual Guid? CityId { get; set; }

    public virtual String Name { get; private set; } = String.Empty;

    public virtual Int32 Age { get; set; }
    public virtual DateTime? Birthday { get; set; }

    [DisableDateTimeNormalization]
    public virtual DateTime? LastActive { get; set; }

    [NotMapped]
    public virtual DateTime? NotMappedDateTime { get; set; }

    public virtual Collection<Phone> Phones { get; set; } = [];

    public virtual DateTime LastActiveTime { get; set; }

    public virtual DateTime HasDefaultValue { get; set; }

    public Int32 EntityVersion { get; set; }

    private Person()
    {
    }

    public Person(Guid id, String name, Int32 age, Guid? tenantId = null, Guid? cityId = null)
        : base(id)
    {
        Name = name;
        Age = age;
        TenantId = tenantId;
        CityId = cityId;

        Phones = [];
    }

    public virtual void ChangeName(String name)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var oldName = Name;
        Name = name;

        AddLocalEvent(
            new PersonNameChangedEvent
            {
                Person = this,
                OldName = oldName
            }
        );

        AddDistributedEvent(
            new PersonNameChangedEto
            {
                Id = Id,
                OldName = oldName,
                NewName = Name,
                TenantId = TenantId
            }
        );
    }
}
