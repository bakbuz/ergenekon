using Ergenekon.Application.SearchTerms.Services;
using Ergenekon.Domain.Entities;
using Ergenekon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Ergenekon.Infrastructure.Services;

public class SearchTermService : ISearchTermService
{
    private readonly ApplicationDbContext _ctx;
    private readonly ILogger<SearchTermService> _logger;

    public SearchTermService([NotNull] ApplicationDbContext ctx, ILogger<SearchTermService> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    /*public virtual IPagedList<SearchTermReportLine> GetStats(int skip = 0, int limit = int.MaxValue)
    {
        var query = (from st in _ctx.SearchTerms
                     group st by st.Keyword into groupedResult
                     select new
                     {
                         Keyword = groupedResult.Key,
                         Count = groupedResult.Sum(o => o.Count)
                     })
                    .OrderByDescending(m => m.Count)
                    .Select(r => new SearchTermReportLine
                    {
                        Keyword = r.Keyword,
                        Count = r.Count
                    });

        var result = new PagedList<SearchTermReportLine>(query, skip, limit);
        return result;
    }*/

    public async Task<SearchTerm?> GetSearchTermByIdAsync(int searchTermId)
    {
        if (searchTermId == 0)
            return null;

        return await _ctx.SearchTerms.SingleOrDefaultAsync(x => x.Id == searchTermId);
    }

    public async Task<SearchTerm?> GetSearchTermByKeywordAsync(string keyword)
    {
        if (string.IsNullOrEmpty(keyword))
            return null;

        var query = from st in _ctx.SearchTerms
                    where st.Keyword == keyword
                    orderby st.Id
                    select st;

        return await query.FirstOrDefaultAsync();
    }

    public async Task InsertSearchTermAsync(SearchTerm searchTerm)
    {
        if (searchTerm == null)
            throw new ArgumentNullException(nameof(searchTerm));

        try
        {
            _ctx.SearchTerms.Add(searchTerm);
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("InsertSearchTerm", ex);
            throw;
        }
    }

    public async Task UpdateSearchTermAsync(SearchTerm searchTerm)
    {
        if (searchTerm == null)
            throw new ArgumentNullException(nameof(searchTerm));

        try
        {
            _ctx.SearchTerms.Update(searchTerm);
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("UpdateSearchTerm", ex);
            throw;
        }
    }

    public async Task DeleteSearchTermAsync(SearchTerm searchTerm)
    {
        if (searchTerm == null)
            throw new ArgumentNullException(nameof(searchTerm));

        try
        {
            _ctx.SearchTerms.Remove(searchTerm);
            await _ctx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("DeleteSearchTerm", ex);
            throw;
        }
    }
}