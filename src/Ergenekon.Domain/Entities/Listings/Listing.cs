namespace Ergenekon.Domain.Entities.Listings;

public partial class Listing : BaseAuditableEntity<int>
{
    public Listing()
    {
        Pictures = new HashSet<ListingPicture>();
    }

    public short BreedId { get; set; }

    public int OwnerId { get; set; }

    public string Slug { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Summary { get; set; }

    public decimal? Price { get; set; }

    public int? Currency { get; set; }

    public byte PublicationDuration { get; set; }

    public bool AutoRenewal { get; set; }

    public byte Age { get; set; }

    public Gender Gender { get; set; }

    public byte Type { get; set; }

    public byte Vaccine { get; set; }

    public byte InternalParasite { get; set; }

    public byte ExternalParasite { get; set; }

    public byte DeliveryToOutOfCity { get; set; }

    //public string? PhotoIds { get; set; }

    //public string? VideoIds { get; set; }

    public ushort ProvinceId { get; set; }

    public ushort DistrictId { get; set; }

    public uint NeighborhoodId { get; set; }

    public string? LastIpAddress { get; set; }

    public ListingStatus Status { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Breed Breed { get; set; } = null!;

    public virtual ICollection<ListingPicture> Pictures { get; set; }
}