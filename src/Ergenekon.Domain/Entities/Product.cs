namespace Ergenekon.Domain.Entities;

public class Product : BaseEntity<Guid>
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
