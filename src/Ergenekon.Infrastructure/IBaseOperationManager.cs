using Ergenekon.Common;
using System.Linq.Expressions;

namespace Ergenekon.Infrastructure
{
    internal interface IBaseOperationManager<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IQueryable<TEntity> Queryable { get; }

        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        List<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        void InsertRange(IEnumerable<TEntity> entities);
        Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        long LongCount();
        long LongCount(Expression<Func<TEntity, bool>> predicate);
        Task<long> LongCountAsync(CancellationToken cancellationToken = default);
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        void SoftDelete(IEnumerable<TEntity> entities);
        void SoftDelete(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}