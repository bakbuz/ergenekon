using Ergenekon.Domain;
using Ergenekon.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;

        public CountriesController(ICountryService countryService, IStateProvinceService stateProvinceService)
        {
            _countryService = countryService;
            _stateProvinceService = stateProvinceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("{id}/StateProvinces")]
        public async Task<IActionResult> GetStateProvinces([FromRoute] int id)
        {
            var stateProvinces = await _stateProvinceService.GetStateProvincesAsync(id);
            return Ok(stateProvinces);
        }
    }
}
