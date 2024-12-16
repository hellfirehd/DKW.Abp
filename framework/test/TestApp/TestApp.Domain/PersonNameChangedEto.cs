namespace TestApp;

public class PersonNameChangedEto
{
    public virtual Guid Id { get; set; }

    public virtual Guid? TenantId { get; set; }

    public String OldName { get; set; } = String.Empty;

    public String NewName { get; set; } = String.Empty;
}
