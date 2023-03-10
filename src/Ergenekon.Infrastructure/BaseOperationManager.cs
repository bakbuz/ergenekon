using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Ergenekon.Infrastructure
{
    internal class BaseOperationManager<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private DbSet<TEntity> _entitySet;

        public BaseOperationManager([NotNull] DataContext context)
        {
            _context = context;
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

        public virtual IList<TEntity> GetAll()
        {
            return EntitySet.ToList();
        }

        public virtual IList<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate).ToList();
        }

        public virtual TEntity GetById(object id)
        {
            //return Entities.SingleOrDefault(q => q.Id == id);
            return EntitySet.Find(id);
        }

        public virtual TEntity GetOne(Expression<Func<TEntity, bool>> predicate)
        {
            return EntitySet.Where(predicate).SingleOrDefault();
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                EntitySet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                EntitySet.AddRange(entities);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                EntitySet.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                EntitySet.UpdateRange(entities);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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
                Update(entity);
            }
        }


        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                EntitySet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                EntitySet.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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

        #region Async

        public virtual async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await EntitySet.ToListAsync(cancellationToken);
        }

        public virtual async Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await EntitySet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken)
        {
            //return await Entities.SingleOrDefaultAsync(q => q.Id == id, cancellationToken);
            return await EntitySet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await EntitySet.Where(predicate).SingleOrDefaultAsync(cancellationToken);
        }


        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await EntitySet.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                await EntitySet.AddRangeAsync(entities);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                EntitySet.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                EntitySet.UpdateRange(entities);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                EntitySet.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                EntitySet.RemoveRange(entities);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await EntitySet.CountAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await EntitySet.CountAsync(predicate, cancellationToken);
        }

        public virtual async Task<long> LongCountAsync(CancellationToken cancellationToken)
        {
            return await EntitySet.LongCountAsync(cancellationToken);
        }

        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
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
