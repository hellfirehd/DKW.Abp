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
