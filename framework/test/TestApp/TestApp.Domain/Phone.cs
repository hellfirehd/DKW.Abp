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
