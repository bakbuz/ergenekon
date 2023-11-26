using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Listings.Listings.Queries.GetListings;
using Ergenekon.Application.Listings.Listings.Shared;
using Ergenekon.Application.Storage;
using Ergenekon.Domain.Entities.Listings;
using Ergenekon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Ergenekon.Infrastructure.Services;

public class ListingService : CrudService<Listing, int>, IListingService
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _distributedCache;
    private readonly IFileStorage _fileStorage;

    public ListingService(ApplicationDbContext context, IDistributedCache distributedCache, IFileStorage fileStorage) : base(context)
    {
        _context = context;
        _distributedCache = distributedCache;
        _fileStorage = fileStorage;
    }

    public async Task<ListingSummaryVm> SearchAsync(GetListingsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Listings.Where(q => q.Status == ListingStatus.Published);

        if (request.BreedId > 0)
            query = query.Where(q => q.BreedId == request.BreedId.Value);

        if (request.OwnerId > 0)
            query = query.Where(q => q.OwnerId == request.OwnerId.Value);

        if (request.ProvinceId > 0)
            query = query.Where(q => q.ProvinceId == request.ProvinceId.Value);

        if (request.DistrictId > 0)
            query = query.Where(q => q.DistrictId == request.DistrictId.Value);

        if (request.NeighborhoodId > 0)
            query = query.Where(q => q.NeighborhoodId == request.NeighborhoodId.Value);

        var totalCount = await query.CountAsync(cancellationToken);
        if (totalCount == 0)
            return new ListingSummaryVm();

        var skip = 0;
        if (request.PageIndex > 1)
            skip = (request.PageIndex - 1) * request.PageSize;

        var listings = await query
        //.OrderBy(request.SortingColumn, request.SortingDir == SortingDirection.ASC)
            .OrderByDescending(o => o.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .Select(s => new ListingSummaryItemVm
            {
                Id = s.Id,
                Slug = s.Slug,
                Title = s.Title,
                CreatedAt = s.CreatedAt,
            })
            .ToListAsync(cancellationToken);

        foreach (var listing in listings)
        {
            listing.FeaturedImage = GetFeaturedImageFromCache(listing.Id, cancellationToken);
        }

        var result = new ListingSummaryVm();
        result.Total = totalCount;
        result.Items = listings;

        return result;
    }

    private string GetFeaturedImageFromCache(int listingId, CancellationToken cancellationToken)
    {
        var key = $"featured-image-{listingId}";
        var data = _distributedCache.GetData<string>(key, () =>
        {
            return GetPictureByListingIdAsync(listingId, cancellationToken).Result;
        });
        return data;
    }

    private async Task<string> GetFeaturedImageFromCacheAsync(int listingId, CancellationToken cancellationToken)
    {
        var key = $"featured-image-{listingId}";
        var data = await _distributedCache.GetDataAsync<string>(key, () =>
        {
            return GetPictureByListingIdAsync(listingId, cancellationToken).Result;
        });
        return data;
    }



    public async Task<List<Listing>> GetPublishedListingsAsync(CancellationToken cancellationToken)
    {
        return await _context.Listings.Where(q => q.Status == ListingStatus.Published).ToListAsync(cancellationToken);
    }

    public async Task<Listing?> GetListingByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Listings.Where(q => q.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task InsertListingAsync(Listing listing, CancellationToken cancellationToken)
    {
        if (listing == null)
            throw new ArgumentNullException(nameof(listing));

        _context.Add(listing);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateListingAsync(Listing listing, CancellationToken cancellationToken)
    {
        if (listing == null)
            throw new ArgumentNullException(nameof(listing));

        _context.Update(listing);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteListingAsync(Listing listing, CancellationToken cancellationToken)
    {
        if (listing == null)
            throw new ArgumentNullException(nameof(listing));

        listing.Status = ListingStatus.Deleted;
        listing.DeletedBy = 0;
        listing.DeletedAt = DateTime.Now;

        _context.Update(listing);
        await _context.SaveChangesAsync(cancellationToken);
    }




    public async Task<List<Listing>> GetListingsByOwnerIdAsync(int ownerId, CancellationToken cancellationToken)
    {
        return await _context.Listings.Where(q => q.OwnerId == ownerId).ToListAsync(cancellationToken);
    }

    public async Task<Listing?> GetListingByOwnerIdAsync(int ownerId, int listingId, CancellationToken cancellationToken)
    {
        return await _context.Listings.Where(q => q.OwnerId == ownerId && q.Id == listingId).SingleOrDefaultAsync(cancellationToken);
    }


    #region Pictures

    public async ValueTask<string> GetPictureByListingIdAsync(int listingId, CancellationToken cancellationToken)
    {
        int? pictureId = await _context.ListingPictures.Where(q => q.ListingId == listingId).OrderBy(o => o.DisplayOrder).ThenBy(t => t.Id).Select(s => s.PictureId).FirstOrDefaultAsync(cancellationToken);
        if (pictureId.HasValue)
        {
            var picture = await _context.Pictures.Where(q => q.Id == pictureId).SingleOrDefaultAsync(cancellationToken);
            if (picture != null)
                return _fileStorage.GetAbsolutePath(picture.RelativePath);
        }
        return string.Empty;
    }

    public async ValueTask<string[]> GetPicturesByListingIdAsync(int listingId, CancellationToken cancellationToken)
    {
        var pictureIds = await _context.ListingPictures.Where(q => q.ListingId == listingId).OrderBy(o => o.DisplayOrder).ThenBy(t => t.Id).Select(s => s.PictureId).ToListAsync(cancellationToken);
        if (pictureIds.Count > 0)
        {
            string[] pictures = await _context.Pictures.AsNoTracking()
                .Where(q => pictureIds.Contains(q.Id))
                .Select(picture => _fileStorage.GetAbsolutePath(picture.RelativePath))
                .ToArrayAsync(cancellationToken);

            return pictures;
        }
        return new string[0];
    }

    #endregion





    //public async Task ListingMediaCreateAsync(int listingId, int[] pictureIds, CancellationToken cancellationToken)
    //{
    //    var medias = new List<ListingMedia>();
    //    foreach (var pictureId in pictureIds)
    //    {
    //        medias.Add(new ListingMedia
    //        {
    //            ListingId = listingId,
    //            PictureId = pictureId,
    //            CreatedAt = DateTime.Now,
    //        });
    //    }

    //    _context.ListingMedias.AddRange(medias);
    //    await _context.SaveChangesAsync(cancellationToken);
    //}

    //public async Task ListingMediaDeleteAsync(int listingId, int pictureId, CancellationToken cancellationToken)
    //{
    //    await _context.ListingMedias.Where(q => q.ListingId == listingId && q.PictureId == pictureId).ExecuteDeleteAsync(cancellationToken);
    //}

    public async Task ListingPictureCreateAsync(int listingId, int[] pictureIds, CancellationToken cancellationToken)
    {
        var pictures = new List<ListingPicture>();
        foreach (var pictureId in pictureIds)
        {
            pictures.Add(new ListingPicture
            {
                ListingId = listingId,
                PictureId = pictureId,
                CreatedAt = DateTime.Now,
            });
        }

        _context.ListingPictures.AddRange(pictures);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ListingPictureDeleteAsync(int listingId, int pictureId, CancellationToken cancellationToken)
    {
        await _context.ListingPictures.Where(q => q.ListingId == listingId && q.PictureId == pictureId).ExecuteDeleteAsync(cancellationToken);
    }

    public async ValueTask<string[]> ListingPictureSelectAsync(int listingId, CancellationToken cancellationToken)
    {
        return await GetPicturesByListingIdAsync(listingId, cancellationToken);
    }
}
