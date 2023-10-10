using Ergenekon.Application.Listings.Listings.Queries.GetListings;
using Ergenekon.Application.Listings.Listings.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[AllowAnonymous]
public class ListingsController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ListingSummaryVm>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseErrors))]
    public async Task<ActionResult<List<ListingSummaryVm>>> GetListings([FromQuery] GetListingsQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query));
    }
}
