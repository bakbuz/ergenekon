using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Territory.Queries.GetCountries;
using Ergenekon.Application.Territory.Queries.GetGetDistrictsByProvinceId;
using Ergenekon.Application.Territory.Queries.GetNeighborhoodsByDistrictId;
using Ergenekon.Application.Territory.Queries.GetProvincesByCountryId;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

public class TerritoryController : ApiControllerBase
{
    [HttpGet("countries")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1<byte>>))]
    public async Task<IActionResult> GetCountries()
    {
        var result = await Mediator.Send(new GetCountriesQuery());
        return Ok(result);
    }

    [HttpGet("provinces")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1<ushort>>))]
    public async Task<IActionResult> GetProvinces([FromQuery] byte countryId)
    {
        if (countryId <= 0)
            //return BadRequest(new ErrorResponse("Belirtilen kimlik değeri geçersiz: " + countryId));
            throw new ArgumentOutOfRangeException(nameof(countryId));

        return Ok(await Mediator.Send(new GetProvincesByCountryIdQuery(countryId)));
    }

    [HttpGet("districts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1<ushort>>))]
    public async Task<IActionResult> GetDistricts([FromQuery] ushort provinceId)
    {
        if (provinceId <= 0)
            //return BadRequest(new ErrorResponse("Belirtilen kimlik değeri geçersiz: " + provinceId));
            throw new ArgumentOutOfRangeException(nameof(provinceId));

        return Ok(await Mediator.Send(new GetDistrictsByProvinceIdQuery(provinceId)));
    }

    [HttpGet("neighborhoods")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1<uint>>))]
    public async Task<IActionResult> GetNeighborhoods([FromQuery] ushort districtId)
    {
        if (districtId <= 0)
            throw new ArgumentOutOfRangeException(nameof(districtId));

        return Ok(await Mediator.Send(new GetNeighborhoodsByDistrictIdQuery(districtId)));
    }
}
