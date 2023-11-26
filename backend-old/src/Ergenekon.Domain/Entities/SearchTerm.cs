namespace Ergenekon.Domain.Entities;

public class SearchTerm : BaseEntity<int>
{
    public string Keyword { get; set; } = null!;

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }
}
