﻿namespace TestApp;

public class PersonNameChangedEto
{
    public virtual Guid Id { get; set; }

    public virtual Guid? TenantId { get; set; }

    public string OldName { get; set; }

    public string NewName { get; set; }
}
