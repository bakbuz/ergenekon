using Ergenekon.Application.Common.Models;
using Ergenekon.Application.World.Services;
using MediatR;

namespace Ergenekon.Application.World.Queries.GetCountries;

public record GetCountriesQuery : IRequest<List<LookupDto1<byte>>>;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, List<LookupDto1<byte>>>
{
    private readonly IWorldService _worldService;

    public GetCountriesQueryHandler(IWorldService worldService)
    {
        _worldService = worldService;
    }

    public async Task<List<LookupDto1<byte>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        return await _worldService.GetAllCountriesAsync(cancellationToken);
    }
}