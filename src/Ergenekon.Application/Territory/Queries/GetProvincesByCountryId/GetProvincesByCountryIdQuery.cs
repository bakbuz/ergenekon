using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Territory.Services;
using MediatR;

namespace Ergenekon.Application.Territory.Queries.GetProvincesByCountryId;

public record GetProvincesByCountryIdQuery(ushort CountryId) : IRequest<List<LookupDto1<ushort>>>;

public class GetProvincesByCountryIdQueryHandler : IRequestHandler<GetProvincesByCountryIdQuery, List<LookupDto1<ushort>>>
{
    private readonly ITerritoryService _worldService;

    public GetProvincesByCountryIdQueryHandler(ITerritoryService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<ushort>>> Handle(GetProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetProvincesAsync(request.CountryId, cancellationToken);
    }
}
