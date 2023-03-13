using Ergenekon.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Ergenekon.Infrastructure
{
    public abstract class BaseOperationManager<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly DataContext _context;
        private DbSet<TEntity> _entitySet;

        private readonly ILogger _logger;

        public BaseOperationManager([NotNull] DataContext context, [NotNull] ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual IQueryable<TEntity> Queryable => EntitySet;

        protected virtual DbSet<TEntity> EntitySet
        {
            get
            {
                if (_entitySet == null)
                    _entitySet = _context.Set<TEntity>();

                return _entitySet;
            }
        }

        #region Select Operations

        public virtual List<TEntity> GetAll()
        {
            return EntitySet.ToList();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await EntitySet.ToListAsync(cancellationToken);
        }

        public virtual List<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate).ToList();
        }

        public virtual async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await EntitySet.Where(predicate).ToListAsync(cancellationToken);
        }

        //public virtual TEntity GetById(TKey id)
        //{
        //    return EntitySet.Where(q => q.Id == id).SingleOrDefault();
        //}

        //public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        //{
        //    return await EntitySet.SingleOrDefaultAsync(q => q.Id == id, cancellationToken);
        //}

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await EntitySet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        #endregion

        #region Insert Operations

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            EntitySet.Add(entity);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert record");

                throw;
            }
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await EntitySet.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert record");

                throw;
            }
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            EntitySet.AddRange(entities);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert record");

                throw;
            }
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            await EntitySet.AddRangeAsync(entities);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert record");

                throw;
            }
        }

        #endregion

        #region Update Operations

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            EntitySet.Update(entity);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update record");

                throw;
            }
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            EntitySet.Update(entity);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update record");

                throw;
            }
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            EntitySet.UpdateRange(entities);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update record");

                throw;
            }
        }

        public virtual async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            EntitySet.UpdateRange(entities);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update record");

                throw;
            }
        }

        #endregion

        #region Delete Operations

        public virtual void SoftDelete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            (entity as ISoftDelete).Deleted = true;

            Update(entity);
        }

        public virtual void SoftDelete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (TEntity entity in entities)
            {
                (entity as ISoftDelete).Deleted = true;
            }

            UpdateRange(entities);
        }


        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            EntitySet.Remove(entity);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete record");

                throw;
            }
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            EntitySet.Remove(entity);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete record");

                throw;
            }
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            EntitySet.RemoveRange(entities);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete record");

                throw;
            }
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            EntitySet.RemoveRange(entities);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete record");

                throw;
            }
        }

        #endregion

        #region Aggregate Operations

        public virtual int Count()
        {
            return EntitySet.Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Count(predicate);
        }

        public virtual long LongCount()
        {
            return EntitySet.LongCount();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.LongCount(predicate);
        }

        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await EntitySet.CountAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await EntitySet.CountAsync(predicate, cancellationToken);
        }

        public virtual async Task<long> LongCountAsync(CancellationToken cancellationToken = default)
        {
            return await EntitySet.LongCountAsync(cancellationToken);
        }

        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await EntitySet.LongCountAsync(predicate, cancellationToken);
        }

        #endregion

        //public virtual TEntity GetByIdIncluding(int id, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    var query = Entities.Where(q => q.Id == id);
        //    return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).Single();
        //}

        //public virtual async Task<TEntity> GetByIdIncludingAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        //{
        //    var query = Entities.Where(q => q.Id == id);
        //    return await includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).SingleAsync();
        //}
    }
}
