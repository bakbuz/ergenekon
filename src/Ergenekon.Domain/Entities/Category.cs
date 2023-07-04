namespace Ergenekon.Domain.Entities;

public class Category : BaseAuditableEntity<int>
{
    public int ParentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Picture { get; set; }
}
