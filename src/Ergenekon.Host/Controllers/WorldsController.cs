using Ergenekon.Application.World.Queries.GetCountries;
using Ergenekon.Application.World.Queries.GetGetDistrictsByStateProvinceId;
using Ergenekon.Application.World.Queries.GetStateProvincesByCountryId;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

public class WorldsController : ApiControllerBase
{
    [HttpGet("countries")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1>))]
    public async Task<IActionResult> GetCountries()
    {
        var result = await Mediator.Send(new GetCountriesQuery());
        return Ok(result);
    }

    [HttpGet("countries/{countryId}/StateProvinces")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1>))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetStateProvinces([FromRoute] byte countryId)
    {
        if (countryId <= 0)
            //return BadRequest(new ErrorResponse("Belirtilen kimlik değeri geçersiz: " + countryId));
            throw new ArgumentOutOfRangeException(nameof(countryId));

        return Ok(await Mediator.Send(new GetStateProvincesByCountryIdQuery(countryId)));
    }

    [HttpGet("countries/{countryId}/StateProvinces/{provinceId}/districts")]
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<LookupDto1>))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetDistricts([FromRoute] short provinceId)
    {
        if (provinceId <= 0)
            //return BadRequest(new ErrorResponse("Belirtilen kimlik değeri geçersiz: " + provinceId));
            throw new ArgumentOutOfRangeException(nameof(provinceId));

        return Ok(await Mediator.Send(new GetGetDistrictsByStateProvinceIdQuery(provinceId)));
    }
}
