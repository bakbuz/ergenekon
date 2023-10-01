using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Territory.Services;
using MediatR;

namespace Ergenekon.Application.Territory.Queries.GetProvincesByCountryId;

public record GetProvincesByCountryIdQuery(byte CountryId) : IRequest<List<LookupDto1<ushort>>>;

public class GetProvincesByCountryIdQueryHandler : IRequestHandler<GetProvincesByCountryIdQuery, List<LookupDto1<ushort>>>
{
    private readonly ITerritoryService _territoryService;

    public GetProvincesByCountryIdQueryHandler(ITerritoryService territoryService)
    {
        _territoryService = territoryService;
    }

    public async Task<List<LookupDto1<ushort>>> Handle(GetProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        return await _territoryService.GetProvincesAsync(request.CountryId, cancellationToken);
    }
}
