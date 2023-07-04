namespace Ergenekon.Domain.Entities;

public class Category : BaseEntity<int>
{
    public int ParentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }
}
