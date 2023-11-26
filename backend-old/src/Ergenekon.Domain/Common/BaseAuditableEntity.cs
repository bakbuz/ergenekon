using System.Text.Json.Serialization;

namespace Ergenekon.Domain.Common;

public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>
{
    [JsonPropertyOrder(96)]
    public string? CreatedBy { get; set; }

    [JsonPropertyOrder(97)]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyOrder(98)]
    public string? LastModifiedBy { get; set; }

    [JsonPropertyOrder(99)]
    public DateTime? LastModifiedAt { get; set; }
}
