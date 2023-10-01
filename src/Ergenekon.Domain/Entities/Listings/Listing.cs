using Patiyuva.Domain.Enums;
using System.Text.Json.Serialization;

namespace Patiyuva.Domain.Entities.Listings;

public partial class Listing
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

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

    public ushort? ProvinceId { get; set; }

    public ushort? DistrictId { get; set; }

    public string? LastIpAddress { get; set; }

    public ListingStatus Status { get; set; }

    [JsonIgnore]
    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public int? UpdatedBy { get; set; }

    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public int? DeletedBy { get; set; }

    [JsonIgnore]
    public DateTime? DeletedAt { get; set; }

    //public virtual User User { get; set; }

    // public virtual ICollection<ListingMedia> Medias { get; set; }
    public virtual ICollection<ListingPicture> Pictures { get; set; }
}