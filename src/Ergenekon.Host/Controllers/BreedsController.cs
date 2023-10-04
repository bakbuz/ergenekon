using Ergenekon.Application.Listings.Breeds.Queries.GetBreeds;
using Ergenekon.Application.Listings.Breeds.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[AllowAnonymous]
public class BreedsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BreedVm>>> GetBreeds()
    {
        return Ok(await Mediator.Send(new GetBreedsQuery()));
    }
}
