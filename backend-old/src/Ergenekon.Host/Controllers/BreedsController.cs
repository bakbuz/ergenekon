using Ergenekon.Application.Listings.Breeds.Queries.GetBreeds;
using Ergenekon.Application.Listings.Breeds.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ergenekon.Host.Controllers;

[AllowAnonymous]
public class BreedsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BreedVm>>> GetBreeds()
    {
        //WriteReq();
        return Ok(await Mediator.Send(new GetBreedsQuery()));
    }

    private void WriteReq()
    {
        //FrozenDictionary<
        var json = JsonSerializer.Serialize(HttpContext.Request.Headers);
        using (var sw = new StreamWriter("req.json"))
        {
            sw.Write(json);
        }
    }
}
