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

    public async Task<List<LookupDto1<ushort>>> GetStateProvincesAsync(byte countryId, CancellationToken cancellationToken)
    {
        var stateProvinces = await _ctx.StateProvinces
            .Where(q => q.CountryId == countryId)
            .OrderBy(o => o.DisplayOrder)
            .ThenBy(o => o.Id)
            .Select(s => new LookupDto1<ushort>(s.Id, s.Name))
            .ToListAsync(cancellationToken);

        return stateProvinces;
    }

    public async Task<List<LookupDto1<ushort>>> GetDistrictsAsync(ushort stateProvinceId, CancellationToken cancellationToken)
    {
        var districts = await _ctx.Districts
            .Where(q => q.StateProvinceId == stateProvinceId)
            .Select(s => new LookupDto1<ushort>(s.Id, s.Name))
            .ToListAsync(cancellationToken);

        return districts;
    }
}
