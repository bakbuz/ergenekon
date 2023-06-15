using Ergenekon.Domain.Common;

namespace Ergenekon.Domain.Entities;

public class Category : Entity<int>
{
    public int ParentId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    public byte[]? Picture { get; set; }
}
