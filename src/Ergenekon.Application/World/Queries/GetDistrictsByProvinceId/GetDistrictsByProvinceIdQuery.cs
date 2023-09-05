using Ergenekon.Application.Common.Models;
using Ergenekon.Application.World.Services;
using MediatR;

namespace Ergenekon.Application.World.Queries.GetGetDistrictsByProvinceId;

public record GetDistrictsByProvinceIdQuery(ushort ProvinceId) : IRequest<List<LookupDto1<ushort>>>;

public class GetGetDistrictsByProvinceIdQueryHandler : IRequestHandler<GetDistrictsByProvinceIdQuery, List<LookupDto1<ushort>>>
{
    private readonly IWorldService _worldService;

    public GetGetDistrictsByProvinceIdQueryHandler(IWorldService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<ushort>>> Handle(GetDistrictsByProvinceIdQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetDistrictsAsync(request.ProvinceId, cancellationToken);
    }
}

