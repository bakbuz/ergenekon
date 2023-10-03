namespace Ergenekon.Domain.Entities.Listings;

public class Breed : BaseAuditableEntity<short>
{
    public short ParentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
