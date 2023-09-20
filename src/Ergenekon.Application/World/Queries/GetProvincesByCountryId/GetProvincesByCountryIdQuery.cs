using Ergenekon.Application.Common.Models;
using Ergenekon.Application.World.Services;
using MediatR;

namespace Ergenekon.Application.World.Queries.GetProvincesByCountryId;

public record GetProvincesByCountryIdQuery(ushort CountryId) : IRequest<List<LookupDto1<ushort>>>;

public class GetProvincesByCountryIdQueryHandler : IRequestHandler<GetProvincesByCountryIdQuery, List<LookupDto1<ushort>>>
{
    private readonly IWorldService _worldService;

    public GetProvincesByCountryIdQueryHandler(IWorldService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<ushort>>> Handle(GetProvincesByCountryIdQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetProvincesAsync(request.CountryId, cancellationToken);
    }
}
