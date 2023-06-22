using Ergenekon.Application.Common.Models;
using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.World.Services;

public interface IWorldService
{
    List<Country> GetAllCountries();

    Task<List<LookupDto1<byte>>> GetAllCountriesAsync(CancellationToken cancellationToken);

    Task<List<LookupDto1<short>>> GetStateProvincesAsync(byte countryId, CancellationToken cancellationToken);

    Task<List<LookupDto1<short>>> GetDistrictsAsync(short stateProvinceId, CancellationToken cancellationToken);
}
