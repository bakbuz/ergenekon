namespace Patiyuva.Domain.Entities.Listings
{
    public enum ListingStatus : byte
    {
        NotSet = 0,
        Draft = 1,
        Published = 2,
        Suspended = 3,
        Deleted = 4
    }
}
