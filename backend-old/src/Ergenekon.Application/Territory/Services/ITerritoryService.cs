using Ergenekon.Application.Common.Models;
using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.Territory.Services;

public interface ITerritoryService
{
    List<Country> GetAllCountries();

    Task<List<LookupDto1<byte>>> GetAllCountriesAsync(CancellationToken cancellationToken);

    Task<List<LookupDto1<ushort>>> GetProvincesAsync(byte countryId, CancellationToken cancellationToken);

    Task<List<LookupDto1<ushort>>> GetDistrictsAsync(ushort provinceId, CancellationToken cancellationToken);

    Task<List<LookupDto1<uint>>> GetNeighborhoodsAsync(ushort districtId, CancellationToken cancellationToken);
}
