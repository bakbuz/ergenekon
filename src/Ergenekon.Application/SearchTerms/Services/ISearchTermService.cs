using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.SearchTerms.Services;

public interface ISearchTermService
{
    //IPagedList<SearchTermReportLine> GetStats(int skip = 0, int limit = int.MaxValue);

    Task<SearchTerm?> GetSearchTermByIdAsync(int searchTermId);

    Task<SearchTerm?> GetSearchTermByKeywordAsync(string keyword);

    Task InsertSearchTermAsync(SearchTerm searchTerm);

    Task UpdateSearchTermAsync(SearchTerm searchTerm);

    Task DeleteSearchTermAsync(SearchTerm searchTerm);
}
