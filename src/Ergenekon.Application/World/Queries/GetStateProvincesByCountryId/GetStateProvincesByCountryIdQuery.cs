using Ergenekon.Application.Common.Models;
using Ergenekon.Application.World.Services;
using MediatR;

namespace Ergenekon.Application.World.Queries.GetStateProvincesByCountryId;

public record GetStateProvincesByCountryIdQuery(byte CountryId) : IRequest<List<LookupDto1<ushort>>>;

public class GetStateProvincesByCountryIdQueryHandler : IRequestHandler<GetStateProvincesByCountryIdQuery, List<LookupDto1<ushort>>>
{
    private readonly IWorldService _worldService;

    public GetStateProvincesByCountryIdQueryHandler(IWorldService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<ushort>>> Handle(GetStateProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetStateProvincesAsync(request.CountryId, cancellationToken);
    }
}
