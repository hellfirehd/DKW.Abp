using System.Diagnostics;

namespace Dkw.Abp;

[DebuggerDisplay("[{Id}]")]
public readonly struct DkwEventId(String id)
{
    public readonly String Id = id;

    public override String ToString() => Id;

    public static implicit operator String(DkwEventId eventId) => eventId.Id;
}
