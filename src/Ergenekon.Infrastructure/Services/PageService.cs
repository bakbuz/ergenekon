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

    public PageService(IApplicationDbContext ctx, ILogger<PageService> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    //private const string PagesAllCacheKey = "ergenekon-pages";
    //private const string PagesByIdCacheKey = PagesAllCacheKey + "-{0}";

    public async Task<List<Page>> GetAllPagesAsync(bool showHidden = false, CancellationToken cancellationToken = default)
    {
        var query = _ctx.Pages.AsQueryable();

        if (!showHidden)
            query = query.Where(t => t.Published);

        var pages = await query.OrderBy(t => t.DisplayOrder).ToListAsync(cancellationToken);

        return pages;
    }

    public async Task<Page?> GetPageByIdAsync(int pageId, CancellationToken cancellationToken)
    {
        if (pageId == 0)
            return await Task.FromResult<Page?>(null);

        return await _ctx.Pages.SingleOrDefaultAsync(x => x.Id == pageId, cancellationToken);
    }

    public async Task<Page?> GetPageBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(slug))
            return await Task.FromResult<Page?>(null);

        var page = await _ctx.Pages.SingleOrDefaultAsync(q => q.Slug == slug, cancellationToken);
        if (page == null)
            return null;

        return page;
    }

    public async Task<Page?> GetPageByCodeAsync(string code, bool showHidden = false, CancellationToken cancellationToken = default)
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
    }
}
