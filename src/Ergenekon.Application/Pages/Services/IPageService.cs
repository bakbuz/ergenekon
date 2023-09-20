using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.Pages.Services;

public interface IPageService
{
    Task<List<Page>> GetAllPagesAsync(bool showHidden = false, CancellationToken cancellationToken = default);

    Task<Page?> GetPageByIdAsync(int pageId, CancellationToken cancellationToken);

    Task<Page?> GetPageBySlugAsync(string slug, CancellationToken cancellationToken);

    Task<Page?> GetPageByCodeAsync(string code, bool showHidden = false, CancellationToken cancellationToken = default);

    Task InsertPageAsync(Page page, CancellationToken cancellationToken);

    Task UpdatePageAsync(Page page, CancellationToken cancellationToken);

    Task DeletePageAsync(Page page, CancellationToken cancellationToken);
}
