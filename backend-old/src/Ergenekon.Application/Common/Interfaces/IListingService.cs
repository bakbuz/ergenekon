using Ergenekon.Application.Listings.Listings.Queries.GetListings;
using Ergenekon.Application.Listings.Listings.Shared;
using Ergenekon.Domain.Entities.Listings;

namespace Ergenekon.Application.Common.Interfaces;

public interface IListingService : ICrudService<Listing, int>
{
    Task<ListingSummaryVm> SearchAsync(GetListingsQuery request, CancellationToken cancellationToken);

    Task<List<Listing>> GetPublishedListingsAsync(CancellationToken cancellationToken);

    Task<Listing?> GetListingByIdAsync(int id, CancellationToken cancellationToken);

    Task InsertListingAsync(Listing listing, CancellationToken cancellationToken);

    Task UpdateListingAsync(Listing listing, CancellationToken cancellationToken);

    Task DeleteListingAsync(Listing listing, CancellationToken cancellationToken);



    Task<List<Listing>> GetListingsByOwnerIdAsync(int ownerId, CancellationToken cancellationToken);

    Task<Listing?> GetListingByOwnerIdAsync(int ownerId, int listingId, CancellationToken cancellationToken);




    //Task ListingMediaCreateAsync(int listingId, int[] pictureIds, CancellationToken cancellationToken);

    //Task ListingMediaDeleteAsync(int listingId, int pictureId, CancellationToken cancellationToken);

    Task ListingPictureCreateAsync(int listingId, int[] pictureIds, CancellationToken cancellationToken);

    Task ListingPictureDeleteAsync(int listingId, int pictureId, CancellationToken cancellationToken);

    ValueTask<string[]> ListingPictureSelectAsync(int listingId, CancellationToken cancellationToken);
}