using Ergenekon.Application.Territory.Queries.GetCountries;
using Ergenekon.Application.Territory.Queries.GetGetDistrictsByProvinceId;
using Ergenekon.Application.Territory.Queries.GetNeighborhoodsByDistrictId;
using Ergenekon.Application.Territory.Queries.GetProvincesByCountryId;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

public class TerritoryOldController : ApiControllerBase
{
    [HttpGet("countries")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1>))]
    public async Task<IActionResult> GetCountries()
    {
        var result = await Mediator.Send(new GetCountriesQuery());
        return Ok(result);
    }

    [HttpGet("countries/{countryId}/provinces")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1>))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetProvinces([FromRoute] byte countryId)
    {
        if (countryId <= 0)
            //return BadRequest(new ErrorResponse("Belirtilen kimlik değeri geçersiz: " + countryId));
            throw new ArgumentOutOfRangeException(nameof(countryId));

        return Ok(await Mediator.Send(new GetProvincesByCountryIdQuery(countryId)));
    }

    [HttpGet("countries/{countryId}/provinces/{provinceId}/districts")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1>))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetDistricts([FromRoute] ushort provinceId)
    {
        if (provinceId <= 0)
            //return BadRequest(new ErrorResponse("Belirtilen kimlik değeri geçersiz: " + provinceId));
            throw new ArgumentOutOfRangeException(nameof(provinceId));

        return Ok(await Mediator.Send(new GetDistrictsByProvinceIdQuery(provinceId)));
    }

    [HttpGet("countries/{countryId}/provinces/{provinceId}/districts/{districtId}/neighborhoods")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1>))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetNeighborhoods([FromRoute] ushort districtId)
    {
        if (districtId <= 0)
            throw new ArgumentOutOfRangeException(nameof(districtId));

        return Ok(await Mediator.Send(new GetNeighborhoodsByDistrictIdQuery(districtId)));
    }
}
