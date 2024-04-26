using Ergenekon.Host.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ergenekon.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ResponseErrors))]
[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseErrors))]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
