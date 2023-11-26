using Ergenekon.Domain.Entities.Media;

namespace Ergenekon.Domain.Entities.Listings;

public class ListingPicture : BaseEntity<int>
{
    public int ListingId { get; set; }

    public int PictureId { get; set; }

    public short DisplayOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Listing Listing { get; set; } = null!;

    public virtual Picture Picture { get; set; } = null!;
}