using Ergenekon.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace Ergenekon.Application.Common.Interfaces;

public interface ICrudService<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync([NotNull] TKey id, CancellationToken cancellationToken = default);

    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
