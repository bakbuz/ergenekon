namespace Ergenekon.Domain.Entities;

public class Product : BaseAuditableEntity<Guid>
{
    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
