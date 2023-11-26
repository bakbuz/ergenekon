namespace Ergenekon.Domain.Entities.Catalog;

public partial class Category : BaseAuditableEntity<int>
{
    public int ParentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Picture { get; set; }
}
