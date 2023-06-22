using Ergenekon.Application.Common.Models;
using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.World.Services;

public interface IWorldService
{
    List<Country> GetAllCountries();

    Task<List<LookupDto1<byte>>> GetAllCountriesAsync(CancellationToken cancellationToken);

    Task<List<LookupDto1<ushort>>> GetStateProvincesAsync(byte countryId, CancellationToken cancellationToken);

    Task<List<LookupDto1<ushort>>> GetDistrictsAsync(ushort stateProvinceId, CancellationToken cancellationToken);
}
