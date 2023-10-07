using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Listings.Listings.Shared;
using MediatR;
using System.ComponentModel;

namespace Ergenekon.Application.Listings.Listings.Queries.GetListings;

public class GetListingsQuery : IRequest<ListingSummaryVm>
{
    public short? BreedId { get; set; }
    public int? OwnerId { get; set; }

    public ushort? ProvinceId { get; set; }
    public ushort? DistrictId { get; set; }
    public uint? NeighborhoodId { get; set; }

    //public string SortingColumn { get; set; } = "Id";
    //public SortingDirection SortingDir { get; set; } = SortingDirection.DESC;

    [DefaultValue(1)]
    public int PageIndex { get; set; } = 1;

    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
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
        var listings = await _listingService.SearchAsync(request, cancellationToken);

        return listings;
    }
}