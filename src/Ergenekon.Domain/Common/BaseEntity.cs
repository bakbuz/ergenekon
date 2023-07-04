using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ergenekon.Domain.Common;

public abstract class BaseEntity : BaseEntity<int>
{
}

public abstract class BaseEntity<TKey> //where TKey : IEquatable<TKey>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey Id { get; set; } = default!;



    private readonly List<BaseEvent> _domainEvents = new();

    [JsonIgnore]
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
