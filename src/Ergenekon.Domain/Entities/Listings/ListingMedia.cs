using Ergenekon.Domain.Entities.Media;

namespace Ergenekon.Domain.Entities.Listings
{
    public class ListingMedia
    {
        public int Id { get; set; }

        public int ListingId { get; set; }

        public int PictureId { get; set; }

        public short DisplayOrder { get; set; }

        public MediaType MediaType { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Listing Listing { get; set; } = null!;
    }
}
