using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Listings.Listings.Shared;
using MediatR;

namespace Ergenekon.Application.Listings.Listings.Queries.GetListings;

public class GetListingsQuery : IRequest<ListingSummaryVm>
{
}

public class GetListingsQueryHandler : IRequestHandler<GetListingsQuery, ListingSummaryVm>
{
    private readonly IListingService _listingService;

    public GetListingsQueryHandler(IListingService listingService)
    {
        _listingService = listingService;
    }

    public async Task<ListingSummaryVm> Handle(GetListingsQuery request, CancellationToken cancellationToken)
    {
        GetFilterRequest filterRequest = new GetFilterRequest();

        var listings = await _listingService.SearchAsync(filterRequest, cancellationToken);

        return listings;
    }
}