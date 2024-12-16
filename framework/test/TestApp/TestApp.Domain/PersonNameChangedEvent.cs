namespace TestApp;

public class PersonNameChangedEvent
{
    public Person Person { get; set; } = default!;

    public String OldName { get; set; } = String.Empty;
}
