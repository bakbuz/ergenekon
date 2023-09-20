using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.World.Services;

public interface IBasarsoftTerritoryService
{
    List<Country> GetCountries();

    List<Province> GetProvinces();

    List<District> GetDistricts();

    List<District> GetDistricts(ushort provinceId);

    List<Neighborhood> GetNeighborhoods();

    List<Neighborhood> GetNeighborhoods(ushort districtId);
}
