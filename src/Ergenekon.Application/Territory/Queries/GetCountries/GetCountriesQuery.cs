using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Territory.Services;
using MediatR;

namespace Ergenekon.Application.Territory.Queries.GetCountries;

public record GetCountriesQuery : IRequest<List<LookupDto1<ushort>>>;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<LookupDto1<ushort>>>
{
    private readonly ITerritoryService _worldService;

    public GetCountriesQueryHandler(ITerritoryService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<ushort>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetAllCountriesAsync(cancellationToken);
    }
}