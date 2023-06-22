using Ergenekon.Application.Common.Models;
using Ergenekon.Application.World.Services;
using MediatR;

namespace Ergenekon.Application.World.Queries.GetGetDistrictsByStateProvinceId;

public record GetGetDistrictsByStateProvinceIdQuery(ushort ProvinceId) : IRequest<List<LookupDto1<ushort>>>;

public class GetGetDistrictsByStateProvinceIdQueryHandler : IRequestHandler<GetGetDistrictsByStateProvinceIdQuery, List<LookupDto1<ushort>>>
{
    private readonly IWorldService _worldService;

    public GetGetDistrictsByStateProvinceIdQueryHandler(IWorldService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<ushort>>> Handle(GetGetDistrictsByStateProvinceIdQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetDistrictsAsync(request.ProvinceId, cancellationToken);
    }
}

