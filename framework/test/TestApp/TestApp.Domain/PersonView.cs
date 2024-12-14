using Volo.Abp.Timing;

namespace TestApp;

public class PersonView
{
    public string Name { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? Birthday { get; set; }

    [DisableDateTimeNormalization]
    public DateTime? LastActive { get; set; }
}
