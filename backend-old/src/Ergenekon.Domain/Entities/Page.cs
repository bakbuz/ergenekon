namespace Ergenekon.Domain.Entities;

public class Page : BaseEntity<ushort>
{
    public string Slug { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public bool Published { get; set; }

    public bool IncludeInNavbar { get; set; }

    public bool IncludeInFooter { get; set; }

    public bool AccessibleWhenSiteClosed { get; set; }

    public int DisplayOrder { get; set; }
}
