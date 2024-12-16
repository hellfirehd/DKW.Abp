using Volo.Abp.Timing;

namespace TestApp;

public class PersonView
{
    public String Name { get; set; } = String.Empty;

    public DateTime CreationTime { get; set; }

    public DateTime? Birthday { get; set; }

    [DisableDateTimeNormalization]
    public DateTime? LastActive { get; set; }
}
