using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Pages.Services;
using Ergenekon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ergenekon.Infrastructure.Services;

public class PageService : IPageService
{
    private readonly IApplicationDbContext _ctx;
    private readonly ILogger<PageService> _logger;
   private readonly ICacheManager _cacheManager;

    public PageService(IApplicationDbContext ctx, ILogger<PageService> logger, ICacheManager cacheManager)
    {
        _ctx = ctx;
        _logger = logger;
        _cacheManager = cacheManager;
    }

    private const string PagesAllCacheKey = "ergenekon-pages";
    private const string PagesByIdCacheKey = PagesAllCacheKey + "-{0}";

    public Task<List<Page>> GetAllPagesAsync(bool showHidden = false, CancellationToken cancellationToken=default)
    {
        var key = string.Format(PagesAllCacheKey, showHidden);
        return _cacheManager.Get(key, () =>
        {
            var query = _ctx.Pages;
            query = query.OrderBy(t => t.DisplayOrder).ThenBy(t => t.Code);

            if (!showHidden)
                query = query.Where(t => t.Published);

            return query.ToListAsync(cancellationToken);
        });
    }

    public Task<Page?> GetPageByIdAsync(int pageId, CancellationToken cancellationToken)
    {
        if (pageId == 0)
            return null;

        var key = string.Format(PagesByIdCacheKey, pageId);
        return _cacheManager.Get(key, async () => await _ctx.Pages.SingleOrDefaultAsync(x => x.Id == pageId, cancellationToken));
    }

    public Task<Page?> GetPageBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(slug))
            return null;

        var key = string.Format(PagesByIdCacheKey, slug);
        return _cacheManager.Get(key, async () =>
        {
            var cached = _urlRecordManager.GetBySlugCached(slug);
            if (cached == null)
                return null;

            return await _ctx.Pages.SingleOrDefaultAsync(x => x.Id == cached.EntityId, cancellationToken);
        });
    }

    public async Task<Page?> GetPageByCodeAsync(string code, bool showHidden = false, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(code))
            return null;

        var query = _ctx.Pages.Where(t => t.Code == code);
        if (!showHidden)
            query = query.Where(c => c.Published);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertPageAsync(Page page, CancellationToken cancellationToken)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        try
        {
            _ctx.Pages.Add(page);
            await _ctx.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("InsertPage", ex);
            throw;
        }

        //cache
        _cacheManager.RemoveByPattern(PagesPatternCacheKey);

        //event notification
        _eventPublisher.EntityInserted(page);
    }

    public async Task UpdatePageAsync(Page page, CancellationToken cancellationToken)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        try
        {
            _ctx.Pages.Update(page);
            await _ctx.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("UpdatePage", ex);
            throw;
        }

        //cache
        _cacheManager.RemoveByPattern(PagesPatternCacheKey);

        //event notification
        _eventPublisher.EntityUpdated(page);
    }

    public async Task DeletePageAsync(Page page, CancellationToken cancellationToken)
    {
        if (page == null)
            throw new ArgumentNullException(nameof(page));

        try
        {
            _ctx.Pages.Remove(page);
            await _ctx.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("DeletePage", ex);
            throw;
        }

        //cache
        _cacheManager.RemoveByPattern(PagesPatternCacheKey);

        //event notification
        _eventPublisher.EntityDeleted(page);
    }
}
