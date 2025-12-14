namespace Wait.Domain.Common;

public abstract class AuditableEntity
{
    public bool IsDeleted { get; init; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly ModifiedAt { get; set; }
}