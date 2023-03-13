using System.Text.Json.Serialization;

namespace Ergenekon.Common
{
    public abstract class Entity<TKey> where TKey : IEquatable<TKey>
    {
        [JsonPropertyOrder(-1)]
        public virtual TKey Id { get; set; } = default!;
    }
}