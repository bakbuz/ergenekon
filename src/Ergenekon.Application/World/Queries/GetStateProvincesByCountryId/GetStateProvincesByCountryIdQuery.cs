using Ergenekon.Application.Common.Models;
using Ergenekon.Application.World.Services;
using MediatR;

namespace Ergenekon.Application.World.Queries.GetStateProvincesByCountryId;

public record GetStateProvincesByCountryIdQuery(byte CountryId) : IRequest<List<LookupDto1<short>>>;

public class GetStateProvincesByCountryIdQueryHandler : IRequestHandler<GetStateProvincesByCountryIdQuery, List<LookupDto1<short>>>
{
    private readonly IWorldService _worldService;

    public GetStateProvincesByCountryIdQueryHandler(IWorldService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<short>>> Handle(GetStateProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetStateProvincesAsync(request.CountryId, cancellationToken);
    }
}
