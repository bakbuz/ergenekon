namespace Patiyuva.Domain.Entities.Listings
{
    public class ListingPicture
    {
        public int Id { get; set; }

        public int ListingId { get; set; }

        public int PictureId { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Listing Listing { get; set; }
    }
}