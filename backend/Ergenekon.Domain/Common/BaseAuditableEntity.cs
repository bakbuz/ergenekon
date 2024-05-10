namespace Ergenekon.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public Guid? CreatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTimeOffset LastModifiedAt { get; set; }
}
