using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Territory.Services;
using MediatR;

namespace Ergenekon.Application.Territory.Queries.GetNeighborhoodsByDistrictId;

public record GetNeighborhoodsByDistrictIdQuery(ushort DistrictId) : IRequest<List<LookupDto1<uint>>>;

public class GetNeighborhoodsByDistrictIdQueryHandler : IRequestHandler<GetNeighborhoodsByDistrictIdQuery, List<LookupDto1<uint>>>
{
    private readonly ITerritoryService _worldService;

    public GetNeighborhoodsByDistrictIdQueryHandler(ITerritoryService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<uint>>> Handle(GetNeighborhoodsByDistrictIdQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetNeighborhoodsAsync(request.DistrictId, cancellationToken);
    }
}

