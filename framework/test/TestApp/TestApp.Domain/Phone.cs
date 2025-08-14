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

using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace TestApp;

[Table("AppPhones")]
public class Phone : Entity<Guid>
{
    public virtual Guid PersonId { get; set; }

    public virtual String Number { get; set; } = String.Empty;

    public virtual PhoneType Type { get; set; }

    private Phone()
    {

    }

    public Phone(Guid personId, String number, PhoneType type = PhoneType.Mobile)
    {
        Id = Guid.NewGuid();
        PersonId = personId;
        Number = number;
        Type = type;
    }

    public override Object[] GetKeys()
    {
        return [PersonId, Number];
    }
}
