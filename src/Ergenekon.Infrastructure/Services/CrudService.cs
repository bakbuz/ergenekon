using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Common;
using Ergenekon.Domain.Entities;
using Ergenekon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Ergenekon.Infrastructure.Services;

public class CrudService<TEntity, TKey> : ICrudService<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public CrudService(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync([NotNull] TKey id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbSet.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (entity.Id != null)
            await UpdateAsync(entity, cancellationToken);
        else
            await CreateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

public class CategoryService : CrudService<Category, int>, ICategoryService
{
    public CategoryService(ApplicationDbContext context)
        : base(context)
    {
    }
}
public class ProductService : CrudService<Product, Guid>, IProductService
{
    public ProductService(ApplicationDbContext context) : base(context)
    {
    }
}