using Ergenekon.Application.Listings.Listings.Queries.GetListings;
using Ergenekon.Application.Listings.Listings.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[AllowAnonymous]
public class ListingsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ListingSummaryVm>>> GetListings()
    {
        return Ok(await Mediator.Send(new GetListingsQuery()));
    }
}
