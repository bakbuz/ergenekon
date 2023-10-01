using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Territory.Services;
using MediatR;

namespace Ergenekon.Application.Territory.Queries.GetCountries;

public record GetCountriesQuery : IRequest<List<LookupDto1<byte>>>;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<LookupDto1<byte>>>
{
    private readonly ITerritoryService _territoryService;

    public GetCountriesQueryHandler(ITerritoryService territoryService)
    {
        _territoryService = territoryService;
    }

    public async Task<List<LookupDto1<byte>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        return await _territoryService.GetAllCountriesAsync(cancellationToken);
    }
}