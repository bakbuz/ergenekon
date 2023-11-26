namespace Ergenekon.Domain.Entities.Media;

public class Video : BaseEntity<int>
{
    public string VideoUrl { get; set; } = null!;
}
