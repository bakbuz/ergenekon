using Ergenekon.Domain.Common;

namespace Ergenekon.Application.Common.Interfaces;

public interface ICrudService<TEntity>
    where TEntity : BaseEntity<Guid>
{
    Task<List<TEntity>> GetAsync(CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
