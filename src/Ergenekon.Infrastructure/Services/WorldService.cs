using Ergenekon.Application.Common.Models;
using Ergenekon.Application.World.Services;
using Ergenekon.Domain.Entities;
using Ergenekon.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Ergenekon.Infrastructure.Services;

public class WorldService : IWorldService
{
    private readonly ApplicationDbContext _ctx;

    public WorldService([NotNull] ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public List<Country> GetAllCountries()
    {
        return _ctx.Countries.ToList();
    }

    public async Task<List<LookupDto1<byte>>> GetAllCountriesAsync(CancellationToken cancellationToken)
    {
        var countries = await _ctx.Countries
            .OrderBy(o => o.DisplayOrder)
            .ThenBy(o => o.Id)
            .Select(s => new LookupDto1<byte>(s.Id, s.Name))
            .ToListAsync(cancellationToken);

        return countries;
    }

    public async Task<List<LookupDto1<ushort>>> GetProvincesAsync(byte countryId, CancellationToken cancellationToken)
    {
        var provinces = await _ctx.Provinces
            .Where(q => q.CountryId == countryId)
            .OrderBy(o => o.DisplayOrder)
            .ThenBy(o => o.Id)
            .Select(s => new LookupDto1<ushort>(s.Id, s.Name))
            .ToListAsync(cancellationToken);

        return provinces;
    }

    public async Task<List<LookupDto1<ushort>>> GetDistrictsAsync(ushort provinceId, CancellationToken cancellationToken)
    {
        var districts = await _ctx.Districts
            .Where(q => q.ProvinceId == provinceId)
            .Select(s => new LookupDto1<ushort>(s.Id, s.Name))
            .ToListAsync(cancellationToken);

        return districts;
    }

    public async Task<List<LookupDto1<uint>>> GetNeighborhoodsAsync(ushort districtId, CancellationToken cancellationToken)
    {
        var neighborhoods = await _ctx.Neighborhoods
           .Where(q => q.DistrictId == districtId)
           .Select(s => new LookupDto1<uint>(s.Id, s.Name))
           .ToListAsync(cancellationToken);

        return neighborhoods;
    }
}
