using Dapper;
using Ergenekon.Application.World.Services;
using Ergenekon.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Ergenekon.Infrastructure.Services;

public class BasarsoftTerritoryService : IBasarsoftTerritoryService
{
    private readonly IConfiguration _configuration;

    public BasarsoftTerritoryService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Country> GetCountries()
    {
        var sql = "SELECT [Id],[Name] FROM [Territory].[Countries]";
        List<Country> data;
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("BasarsoftConnection")))
        {
            sqlConnection.Open();
            data = sqlConnection.Query<Country>(sql).ToList();
            sqlConnection.Close();
        }
        return data;
    }

    public List<Province> GetProvinces()
    {
        var sql = "SELECT [Id],[CountryId],[Name],[UAVT] FROM [Territory].[Provinces]";
        List<Province> data;
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("BasarsoftConnection")))
        {
            sqlConnection.Open();
            data = sqlConnection.Query<Province>(sql).ToList();
            sqlConnection.Close();
        }
        return data;
    }

    public List<District> GetDistricts()
    {
        var sql = "SELECT [Id],[ProvinceId],[Name],[UAVT],[Type] FROM [Territory].[Districts]";
        List<District> data;
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("BasarsoftConnection")))
        {
            sqlConnection.Open();
            data = sqlConnection.Query<District>(sql).ToList();
            sqlConnection.Close();
        }
        return data;
    }

    public List<District> GetDistricts(ushort provinceId)
    {
        var sql = "SELECT [Id],[ProvinceId],[Name],[UAVT],[Type] FROM [Territory].[Districts] WHERE ProvinceId=@ProvinceId";
        List<District> data;
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("BasarsoftConnection")))
        {
            sqlConnection.Open();
            data = sqlConnection.Query<District>(sql, new { ProvinceId = provinceId }).ToList();
            sqlConnection.Close();
        }
        return data;
    }

    public List<Neighborhood> GetNeighborhoods()
    {
        var sql = "SELECT [Id],[DistrictId],[Name],[UAVT],[TypeDefId] FROM [Territory].[Neighborhoods]";
        List<Neighborhood> data;
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("BasarsoftConnection")))
        {
            sqlConnection.Open();
            data = sqlConnection.Query<Neighborhood>(sql).ToList();
            sqlConnection.Close();
        }
        return data;
    }

    public List<Neighborhood> GetNeighborhoods(ushort districtId)
    {
        var sql = "SELECT [Id],[DistrictId],[Name],[UAVT],[TypeDefId] FROM [Territory].[Neighborhoods] WHERE DistrictId=@DistrictId";
        List<Neighborhood> data;
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("BasarsoftConnection")))
        {
            sqlConnection.Open();
            data = sqlConnection.Query<Neighborhood>(sql, new { DistrictId = districtId }).ToList();
            sqlConnection.Close();
        }
        return data;
    }
}
