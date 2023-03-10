namespace Ergenekon.Common
{
    public abstract class Entity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; } = default!;
    }
}